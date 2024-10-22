using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNext : MonoBehaviour
{
    private void OnTriggerEnter(Collider box)
    {
        // Проверяем, пересек ли триггер игрок
        if (box.CompareTag("Player"))
        {
            UnlockLevel(); // Открываем следующий уровень
            LoadNextScene(); // Загружаем следующую сцену
        }
    }

    public void UnlockLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex; // Получаем индекс текущей сцены
        int savedLevels = PlayerPrefs.GetInt("Levels", 1); // Получаем сохранённое количество открытых уровней

        // Если текущий уровень больше или равен сохранённому
        if (currentLevel >= savedLevels)
        {
            PlayerPrefs.SetInt("Levels", currentLevel + 1); // Сохраняем следующий уровень
            PlayerPrefs.Save(); // Сохраняем изменения в PlayerPrefs
        }
    }

    private void LoadNextScene()
    {
        // Переход на следующий уровень
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); // Загружаем следующую сцену
        }
        else
        {
            // Если следующая сцена не существует, вернемся в меню или обработаем это
            SceneManager.LoadScene(0); // Переход на меню
        }
    }
}
