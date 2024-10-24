using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject pointPrefab;
    public Transform spawnArea;
    public Text scoreText;
    public Text timerText; // Текст для отображения таймера
    public float gameDuration = 60f;
    private int score = 0;
    private bool isGameActive = true;
    private float timeRemaining;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        timeRemaining = gameDuration; // Устанавливаем начальное время игры
        SpawnPoints();
        StartCoroutine(GameTimer());
    }

    void SpawnPoints()
    {
        int totalPoints = 20; // Количество кубов
        float yPosition = 0.5f; // Позиция по оси Y (чуть выше плоскости)

        // Получаем реальные размеры плоскости (умножаем масштаб на 10)
        Vector3 planeSize = spawnArea.localScale * 10f;

        // Центральная позиция плоскости
        Vector3 planeCenter = spawnArea.position;

        for (int i = 0; i < totalPoints; i++)
        {
            // Случайные позиции внутри границ плоскости
            float randomX = Random.Range(-planeSize.x / 2, planeSize.x / 2);
            float randomZ = Random.Range(-planeSize.z / 2, planeSize.z / 2);

            // Создаем куб в случайной позиции относительно центра плоскости
            Vector3 spawnPosition = new Vector3(planeCenter.x + randomX, yPosition, planeCenter.z + randomZ);

            // Создаем объект (куб) на рассчитанной позиции
            Instantiate(pointPrefab, spawnPosition, Quaternion.identity);
        }
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
            scoreText.text = "Score: " + score;

            if (score >= 100)
            {
                EndGame();
            }
        }
    }

    void EndGame()
    {
        isGameActive = false;
        scoreText.text = "Game Over! Final Score: " + score;
        Invoke("LoadMainMenu", 3f);
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
