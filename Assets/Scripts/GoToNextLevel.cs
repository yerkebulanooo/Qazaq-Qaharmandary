using UnityEngine;
using UnityEngine.SceneManagement;


public class GoToNext : MonoBehaviour
{

/*************  ✨ Codeium Command 🌟  *************/
    private void OnTriggerEnter(Collider box)
    {
        if (box.CompareTag("Player"))
        {
            UnlockLevel(); // Разблокировка уровня
            SceneManager.LoadScene(0); // Возврат в меню (индекс 0)
        }
    }

    public void UnlockLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex; // Получаем индекс текущего уровня
        int savedLevels = PlayerPrefs.GetInt("Levels", 1); // Получаем количество открытых уровней

        // Разблокировка следующего уровня
        if (currentLevel == savedLevels) // Если игрок завершил текущий уровень
        {
            PlayerPrefs.SetInt("Levels", savedLevels + 1); // Разблокируем следующий уровень
            PlayerPrefs.Save(); // Сохраняем изменения
            Debug.Log($"Теперь открыт уровень {savedLevels + 1}.");
        }
    }
}
