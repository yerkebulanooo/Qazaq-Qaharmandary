using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShootingController : MonoBehaviour
{
    public GameObject arrowPrefab; // Префаб стрелы
    public Transform shootPoint; // Точка стрельбы
    public float shootForce = 1000f; // Сила выстрела
    public int score = 0; // Счет
    public Text scoreText; // UI текст для счета
    public AudioClip shootSound; // Звук выстрела
    private AudioSource audioSource; // Аудиоисточник

    public float rotationSpeed = 0.000005f; // или попробуйте 0.000002f

    private float currentXRotation = 0f; // Текущий угол вращения по оси X (зафиксированный)
    private float currentYRotation = 0f; // Текущий угол вращения по оси Y
    private float currentZRotation = 0f; // Текущий угол вращения по оси Z
    private bool isAiming = false; // Состояние прицеливания

    private Animator animator; // Ссылка на Animator

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource не найден на объекте " + gameObject.name);
        }

        animator = GetComponent<Animator>(); // Получаем компонент Animator
        if (animator == null)
        {
            Debug.LogError("Animator не найден на объекте " + gameObject.name);
        }

        UpdateScoreText(); // Обновляем текст счета
        SetIdleAnimation(); // Устанавливаем начальное состояние - Idle
    }

    void Update()
    {
        HandleTouchInput(); // Обрабатываем касания
    }

    public void Shoot()
    {
        if (!isAiming) // Проверяем, не наводим ли мы лук
        {
            Debug.Log("Shoot method called");

            if (arrowPrefab == null || shootPoint == null)
            {
                Debug.LogError("Arrow Prefab или Shoot Point не установлены!");
                return;
            }

            GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation); // Создаем стрелу
            Rigidbody rb = arrow.GetComponent<Rigidbody>(); // Получаем Rigidbody стрелы

            if (rb == null)
            {
                Debug.LogError("Rigidbody не найден на стрелe!");
                Destroy(arrow);
                return;
            }

            rb.AddForce(shootPoint.forward * shootForce); // Применяем силу к стреле

            // Проигрываем звук выстрела
            if (audioSource != null && shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }
            else
            {
                Debug.LogWarning("AudioSource или звук выстрела не установлены!");
            }

            animator.SetTrigger("isShooting"); // Запускаем анимацию выстрела
            SetIdleAnimation(); // Возвращаемся в состояние Idle после выстрела
        }
    }

    public void AddScore(int points)
    {
        score += points; // Добавляем очки
        UpdateScoreText(); // Обновляем текст счета
            int currentLevel = SceneManager.GetActiveScene().buildIndex; // Получаем индекс текущего уровня
        int savedLevels = PlayerPrefs.GetInt("Levels", 1); // Получаем количество открытых уровней

        if (score >= 30) // Проверяем, не достиг ли счет 100
        {
                   if (currentLevel == savedLevels) // Если игрок завершил текущий уровень
        {
            PlayerPrefs.SetInt("Levels", savedLevels + 1); // Разблокируем следующий уровень
            PlayerPrefs.Save(); // Сохраняем изменения
            Debug.Log($"Теперь открыт уровень {savedLevels + 1}.");
        }   
            LoadMainScene(); // Загружаем главную сцену
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Очки: " + score; // Обновляем текст
    }

    void LoadMainScene()
    {
        SceneManager.LoadScene(0); // Загружаем сцену 0
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0) // Проверяем, есть ли касание
        {
            Touch touch = Input.GetTouch(0); // Получаем первое касание

            // Логика прицеливания при удержании экрана слева
            if (touch.position.x < Screen.width / 2)
            {
                if (touch.phase == TouchPhase.Began) // Если касание началось
                {
                    isAiming = true; // Устанавливаем состояние прицеливания
                    SetIdleAnimation(); // Убедитесь, что анимация Idle установлена
                }
                else if (touch.phase == TouchPhase.Moved && isAiming) // Если прицеливаемся и касание перемещается
                {
                    float yRotationAmount = Mathf.Abs(touch.deltaPosition.x) > 1f ? touch.deltaPosition.x * rotationSpeed : 0f;
float zRotationAmount = Mathf.Abs(touch.deltaPosition.y) > 1f ? touch.deltaPosition.y * rotationSpeed : 0f;
    

                    currentYRotation += yRotationAmount; // Обновляем угол по оси Y
                    currentZRotation += zRotationAmount; // Обновляем угол по оси Z

                    // Ограничиваем поворот в указанных диапазонах
                    currentYRotation = Mathf.Clamp(currentYRotation, 60f, 120f); // Диапазон Y
                    currentZRotation = Mathf.Clamp(currentZRotation, -10f, 10f); // Диапазон Z

                    // Применяем вращение, фиксируя X и используя текущие Y и Z
                    transform.localRotation = Quaternion.Euler(currentXRotation, currentYRotation, currentZRotation);
                }
                else if (touch.phase == TouchPhase.Ended && isAiming) // Если касание закончилось
                {
                    isAiming = false; // Устанавливаем состояние прицеливания в false
                    Shoot(); // Выполняем выстрел при отпускании экрана
                }
            }
        }
        else
        {
            SetIdleAnimation(); // Возвращаемся в состояние Idle, если касаний нет
        }
    }

    void SetIdleAnimation()
    {
        animator.SetBool("isIdle", true); // Устанавливаем флаг idle
        animator.SetBool("isShooting", false); // Сбрасываем флаг выстрела, чтобы вернуться в idle
    }
}
