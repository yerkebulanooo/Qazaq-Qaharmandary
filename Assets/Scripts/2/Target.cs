using UnityEngine;

public class Target : MonoBehaviour
{
    public int points = 10; // Очки за попадание
    private ShootingController shootingController;

    void Start()
    {
        shootingController = FindObjectOfType<ShootingController>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow")) // Проверяем, попала ли стрела в цель
        {
            shootingController.AddScore(points); // Добавляем очки
            Destroy(gameObject); // Удаляем цель после попадания
        }
    }
}
