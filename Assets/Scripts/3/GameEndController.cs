using UnityEngine;
using UnityEngine.SceneManagement; // Для загрузки сцен

public class GameEndController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected with: " + other.name); // Для отладки
        // Проверка, если соприкасается с игроком
        if (other.CompareTag("Player"))
        {
            LoadGameOverScene();
        }
    }

    void LoadGameOverScene()
    {
        SceneManager.LoadScene(0); // Переход на сцену 0
    }
}
