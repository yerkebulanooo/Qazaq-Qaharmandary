using UnityEngine;

public class PlayerController3 : MonoBehaviour
{
    public float speed = 10f;
    public float laneDistance = 3f;
    private int laneIndex = 1; // Начальная дорожка (левая - 0, центр - 1, правая - 2)

    private Vector2 startTouchPosition, endTouchPosition;
    private bool swipeLeft, swipeRight;
    private float minSwipeDistance = 100f; // Минимальное расстояние для определения свайпа

    void Update()
    {
        // Постоянное движение вперед
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Проверка свайпов
        CheckSwipe();

        // Перемещение между дорожками
        if (swipeLeft) MoveLane(-1);
        if (swipeRight) MoveLane(1);
    }

    void CheckSwipe()
    {
        swipeLeft = swipeRight = false;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                Vector2 swipeDelta = endTouchPosition - startTouchPosition;

                // Проверка направления и расстояния свайпа
                if (swipeDelta.magnitude > minSwipeDistance)
                {
                    if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                    {
                        // Свайп вправо или влево
                        if (swipeDelta.x > 0) swipeRight = true;
                        else swipeLeft = true;
                    }

                    // Отладка свайпа
                    Debug.Log($"Swipe detected: Left = {swipeLeft}, Right = {swipeRight}");
                }
            }
        }
    }

    void MoveLane(int direction)
    {
        laneIndex = Mathf.Clamp(laneIndex + direction, 0, 2);
        Vector3 targetPosition = new Vector3(laneIndex * laneDistance - laneDistance, transform.position.y, transform.position.z);
        transform.position = targetPosition;
    }
}
