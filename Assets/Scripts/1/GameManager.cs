using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject pointPrefab; // Объект для очков (если нужен)
    public Transform spawnArea; // Область спавна (если нужен)
    public Canvas canvas; // Ссылка на Canvas
    public Text scoreText; // Текст для отображения очков
    public Text timerText; // Текст для отображения таймера
    public float gameDuration = 60f; // Длительность игры
    private int score = 0; // Очки игрока
    private bool isGameActive = true; // Статус игры
    private float timeRemaining; // Оставшееся время



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Убедитесь, что GameManager не сохраняется между сценами
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        score = 0; // Сбрасываем очки
        isGameActive = true; // Активируем игру
        timeRemaining = gameDuration; // Устанавливаем начальное время игры
        UpdateScoreText(); // Обновляем текст очков
        UpdateTimerText(); // Обновляем текст таймера
        StartCoroutine(GameTimer()); // Запускаем таймер
    }

    IEnumerator GameTimer()
    {
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerText();
            yield return null;
        }
        EndGame();
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds); // Обновляем текст таймера
    }

    public void AddScore(int points)
    {
        if (isGameActive)
        {
            score += points;
            UpdateScoreText(); // Обновляем текст очков

            if (score >= 100)
            {
                EndGame();
            }
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    void EndGame()
    {
        isGameActive = false;
        scoreText.text = "Game Over! Final Score: " + score;
        int currentLevel = SceneManager.GetActiveScene().buildIndex; // Получаем индекс текущего уровня
        int savedLevels = PlayerPrefs.GetInt("Levels", 1); // Получаем количество открытых уровней
        // Удаляем или скрываем Canvas и другие элементы


        // Удаляем или скрываем Canvas и другие элементы

        if (canvas != null)
        {
            canvas.gameObject.SetActive(false); // Деактивируем Canvas
        }

        // Запланируем завершение сцены
        if (score >= 100)
        {

               if (currentLevel == savedLevels) // Если игрок завершил текущий уровень

        {
            PlayerPrefs.SetInt("Levels", savedLevels + 1); // Разблокируем следующий уровень
            PlayerPrefs.Save(); // Сохраняем изменения
            Debug.Log($"Теперь открыт уровень {savedLevels + 1}.");

        }   

            Invoke("LoadMainMenu", 1f); // Переход на главную сцену
        }
        else
        {
            Invoke("RestartScene", 1f); // Перезагрузка сцены
        }
    }

    void RestartScene()
    {
        // Удаляем GameManager, если он не должен сохраняться
        Destroy(gameObject); 

        // Перезапуск текущей сцены
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadMainMenu()
    {
        // Удаляем GameManager, если он не должен сохраняться
        Destroy(gameObject); 

        // Переход на главную сцену
        SceneManager.LoadScene(0); 
    }
}
