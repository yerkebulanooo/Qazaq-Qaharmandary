using UnityEngine;
using UnityEngine.UI; // Убедитесь, что у вас есть ссылка на UI

public class CubeMovement : MonoBehaviour
{
    public float speed = 5f; // Скорость передвижения
    public Joystick joystick; // Ссылка на ваш джойстик

    void Update()
    {
        // Получаем ввод пользователя из джойстика
        float moveHorizontal = joystick.Horizontal; // Влево/вправо
        float moveVertical = joystick.Vertical;     // Вперёд/назад

        // Создаём вектор направления на основе ввода
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        // Передвигаем куб в этом направлении
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
