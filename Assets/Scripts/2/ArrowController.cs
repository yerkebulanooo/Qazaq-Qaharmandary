using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public int scoreValue = 10; // Количество очков за попадание
    private ShootingController shootingController; // Ссылка на ShootingController

    void Start()
    {
        // Найдем ShootingController в сцене
        shootingController = FindObjectOfType<ShootingController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, столкнулась ли стрела с мишенью
        if (other.CompareTag("Target")) // Убедитесь, что мишень имеет тег "Target"
        {
            Debug.Log("Попадание в мишень!"); // Сообщение об удачном попадании

            // Добавляем очки в ShootingController
            if (shootingController != null)
            {
                shootingController.AddScore(scoreValue); // Добавляем очки
            }

            Destroy(gameObject); // Удаляем стрелу после попадания
        }
    }
}
