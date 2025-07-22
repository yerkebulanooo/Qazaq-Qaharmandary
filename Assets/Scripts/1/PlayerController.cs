using UnityEngine;
using UnityEngine.UI; // Убедитесь, что у вас есть ссылка на UI

public class CubeMovement : MonoBehaviour
{
    public float speed = 5f; // Скорость передвижения
    public Joystick joystick; // Ссылка на ваш джойстик
    private Animator animator; // Ссылка на компонент Animator
    private string currentAnimation; // Текущая анимация

    void Start()
    {
        // Получаем компонент Animator
        animator = GetComponent<Animator>();

        // Проверка на наличие Animator
        if (animator == null)
        {
            Debug.LogError("Компонент Animator отсутствует у объекта: " + gameObject.name);
        }
    }

    void Update()
    {
        // Получаем ввод пользователя из джойстика
        float moveHorizontal = joystick.Horizontal; // Влево/вправо (по оси Z)
        float moveVertical = joystick.Vertical;     // Вверх/вниз (по оси Y)

        // Создаём вектор направления на основе ввода
        Vector3 movement = new Vector3(0, moveVertical, -moveHorizontal); // Инвертируем движение по оси Z

        // Передвигаем объект в этом направлении
        transform.Translate(movement * speed * Time.deltaTime);

        // Проверяем, движется ли объект
        if (movement.magnitude > 0)
        {
            // Если объект движется, воспроизводим анимацию плавания
            if (animator != null) // Проверяем, что animator существует
            {
                if (currentAnimation != "swimming2")
                {
                    animator.Play("swimming2"); // Воспроизводим анимацию плавания
                    currentAnimation = "swimming2"; // Запоминаем текущую анимацию
                }
            }
        }
        else
        {
            // Если объект не движется, воспроизводим анимацию стоя
            if (animator != null) // Проверяем, что animator существует
            {
                if (currentAnimation != "swimming1")
                {
                    animator.Play("swimming1"); // Воспроизводим анимацию стоя
                    currentAnimation = "swimming1"; // Запоминаем текущую анимацию
                }
            }
        }
    }
}