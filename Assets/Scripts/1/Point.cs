using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Проверяем, что столкновение с игроком
        {
            GameManager.Instance.AddScore(5); // Добавляем 5 очков
            Destroy(gameObject); // Уничтожаем куб
        }
    }
}
