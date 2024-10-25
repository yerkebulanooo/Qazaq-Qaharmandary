using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float initialSpeed = 12f;  // Начальная скорость врага
    public float speedDecreaseRate = 0.5f; // Скорость уменьшения
    private float currentSpeed;

    void Start()
    {
        currentSpeed = initialSpeed;
    }

    void Update()
    {
        // Уменьшение скорости врага
        if (currentSpeed > 0)
        {
            currentSpeed -= speedDecreaseRate * Time.deltaTime;
        }
        else
        {
            currentSpeed = 0; // Не даем скорости стать отрицательной
        }

        // Движение врага вперед
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }
}
