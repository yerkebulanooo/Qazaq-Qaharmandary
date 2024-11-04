using UnityEngine;
using UnityEngine.SceneManagement; // Для загрузки сцен

public class ObstacleController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Проверка, если соприкасается с игроком
        if (other.CompareTag("Player"))
        {
            Debug.Log("Игрок столкнулся с препятствием! Перезагрузка сцены.");
            ReloadScene();
        }
    }

    void ReloadScene()
    {
        // Перезагрузка текущей сцены
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
