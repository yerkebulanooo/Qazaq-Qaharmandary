using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public float initialSpeed = 12f;  // Начальная скорость врага
    public float speedDecreaseRate = 0.5f; // Скорость уменьшения
    private float currentSpeed;

    void Start()
    {
        currentSpeed = 0; // Устанавливаем начальную скорость в 0, пока враг не начнет двигаться

        // Устанавливаем ротацию по оси Y на 0
        transform.rotation = Quaternion.Euler(0, 0, 0);

        StartCoroutine(StartMovement()); // Запускаем корутину для управления поведением врага
    }

    void Update()
    {
        // Уменьшение скорости врага
        if (currentSpeed > 0)
        {
            currentSpeed -= speedDecreaseRate * Time.deltaTime; // Уменьшаем скорость
        }
        else
        {
            currentSpeed = 0; // Не даем скорости стать отрицательной
        }

        // Движение врага вперед
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime); // Двигаем врага вперед
    }

    private IEnumerator StartMovement()
    {
        // Начинаем движение
        currentSpeed = initialSpeed; // Устанавливаем начальную скорость для движения
        yield return null;
    }
}
