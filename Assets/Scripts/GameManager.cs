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
    public float gameDuration = 60f;
    private int score = 0;
    private bool isGameActive = true;

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
        SpawnPoints();
        StartCoroutine(GameTimer());
    }

    void SpawnPoints()
    {
        for (int i = 0; i < 20; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnArea.localScale.x / 2, spawnArea.localScale.x / 2),
                0.5f,
                Random.Range(-spawnArea.localScale.z / 2, spawnArea.localScale.z / 2)
            );

            Instantiate(pointPrefab, randomPosition, Quaternion.identity);
        }
    }

    IEnumerator GameTimer()
    {
        yield return new WaitForSeconds(gameDuration);
        EndGame();
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
