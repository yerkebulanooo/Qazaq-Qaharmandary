using UnityEngine;

public class PlayerController3 : MonoBehaviour
{
    public float laneDistance = 2f; // Расстояние между дорожками
    public float jumpHeight = 2f;   // Высота прыжка
    public float jumpDuration = 0.4f; // Длительность прыжка
    private int currentLane = 1;    // Текущая дорожка (0 - левая, 1 - центральная, 2 - правая)
    private Vector3 targetPosition; // Целевая позиция для перемещения
    private bool isJumping = false; // Флаг прыжка
    private Vector3 startPosition;  // Стартовая позиция игрока

    // Для свайпа
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool isSwipe = false;

    void Start()
    {
        // Устанавливаем начальную позицию игрока (например, на центральной дорожке)
        startPosition = transform.position;
        currentLane = 1; // Начинаем с центральной дорожки
        SetLanePosition(); // Устанавливаем позицию в зависимости от текущей дорожки
    }

    void Update()
    {
        // Перемещаем игрока по оси X в целевую позицию
        if (!isJumping && transform.position.x != targetPosition.x)
        {
            transform.position = new Vector3(
                Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 10f).x,
                transform.position.y,
                transform.position.z
            );
        }

        // Обработка свайпов
        DetectSwipe();
    }

    void DetectSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
                isSwipe = true;
            }
            else if (touch.phase == TouchPhase.Ended && isSwipe)
            {
                endTouchPosition = touch.position;
                HandleSwipe();
                isSwipe = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) // Для тестирования на ПК
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) // Для тестирования на ПК
        {
            MoveRight();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) // Прыжок для тестирования на ПК
        {
            Jump();
        }
    }

    void HandleSwipe()
    {
        float swipeDistanceX = endTouchPosition.x - startTouchPosition.x;
        float swipeDistanceY = endTouchPosition.y - startTouchPosition.y;

        if (Mathf.Abs(swipeDistanceX) > Mathf.Abs(swipeDistanceY) && Mathf.Abs(swipeDistanceX) > 100) // Минимальная длина свайпа по горизонтали
        {
            if (swipeDistanceX > 0 && currentLane < 2) // Свайп вправо
            {
                MoveRight();
            }
            else if (swipeDistanceX < 0 && currentLane > 0) // Свайп влево
            {
                MoveLeft();
            }
        }
        else if (Mathf.Abs(swipeDistanceY) > 100 && swipeDistanceY > 0) // Свайп вверх
        {
            Jump();
        }
    }

    void MoveLeft()
    {
        if (currentLane > 0)
        {
            currentLane--;
            SetLanePosition();
        }
    }

    void MoveRight()
    {
        if (currentLane < 2)
        {
            currentLane++;
            SetLanePosition();
        }
    }

    void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            Vector3 jumpTarget = new Vector3(transform.position.x, transform.position.y + jumpHeight, transform.position.z);
            StartCoroutine(PerformJump(jumpTarget));
        }
    }

    System.Collections.IEnumerator PerformJump(Vector3 jumpTarget)
    {
        float elapsedTime = 0f;

        // Прыжок вверх
        while (elapsedTime < jumpDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float newY = Mathf.Lerp(transform.position.y, jumpTarget.y, elapsedTime / (jumpDuration / 2));
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            yield return null;
        }

        elapsedTime = 0f;

        // Возвращение вниз
        while (elapsedTime < jumpDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float newY = Mathf.Lerp(transform.position.y, startPosition.y, elapsedTime / (jumpDuration / 2));
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            yield return null;
        }

        isJumping = false;
    }

    void SetLanePosition()
    {
        // Обновляем целевую позицию по оси X в зависимости от текущей дорожки
        targetPosition = new Vector3(startPosition.x + (currentLane - 1) * laneDistance, transform.position.y, transform.position.z);
    }
}
