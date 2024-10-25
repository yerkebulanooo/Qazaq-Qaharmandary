using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Игрок
    public Vector3 offset = new Vector3(0, 20, 10); // Смещение камеры
    public float smoothSpeed = 0.125f; // Скорость плавности камеры

    void LateUpdate()
    {
        // Рассчитываем желаемую позицию камеры
        Vector3 desiredPosition = player.position + offset;

        // Плавное перемещение камеры к желаемой позиции
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Обновляем позицию камеры
        transform.position = smoothedPosition;

        // Если хотите, чтобы камера смотрела на игрока, можно раскомментировать эту строку:
        // transform.LookAt(player);
    }
}
