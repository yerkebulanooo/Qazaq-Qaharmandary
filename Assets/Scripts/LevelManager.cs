using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    int LevelUnlock; // Количество разблокированных уровней
    public Button[] buttons; // Массив кнопок для уровней

    void Start()
    {
        // Сбрасываем PlayerPrefs для отладки (удалите или закомментируйте в финальной версии)
        // PlayerPrefs.DeleteAll(); // Удаляет все сохраненные данные (для отладки)

        // Получаем количество открытых уровней, по умолчанию 1
        LevelUnlock = PlayerPrefs.GetInt("Levels", 1); 

        // Делаем все кнопки неактивными
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false; 
        }

        // Активируем кнопки в зависимости от разблокированных уровней
        for (int i = 0; i < LevelUnlock; i++)
        {
            if (i < buttons.Length)
            {
                buttons[i].interactable = true; // Включаем кнопку для разблокированного уровня
            }
        }
    }

    public void loadLevel(int LevelIndex)
    {
        SceneManager.LoadScene(LevelIndex);
    }
}
