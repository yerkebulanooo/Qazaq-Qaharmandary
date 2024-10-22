using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public float speed = 5f; // Скорость передвижения

    void Update()
    {
        // Получаем ввод пользователя
        float moveHorizontal = Input.GetAxis("Horizontal"); // Влево/вправо: A/D или стрелки влево/вправо
        float moveVertical = Input.GetAxis("Vertical");     // Вперёд/назад: W/S или стрелки вверх/вниз

        // Создаём вектор направления на основе ввода
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        // Передвигаем куб в этом направлении
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
