using UnityEngine;

public class PlayerController3 : MonoBehaviour
{
    public float speed = 10f;
    public float laneDistance = 3f;
    private int laneIndex = 1;

    public float jumpForce = 10f;
    private Rigidbody rb;

    private Vector2 startTouchPosition, endTouchPosition;
    private bool swipeLeft, swipeRight, swipeUp;
    private float minSwipeDistance = 100f;

    private bool isJumping = false;

    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        CheckSwipe();

        if (swipeLeft) MoveLane(-1);
        if (swipeRight) MoveLane(1);

        if (swipeUp && !isJumping)
        {
            Jump();
        }

        UpdateAnimations();
    }

    void CheckSwipe()
    {
        swipeLeft = swipeRight = swipeUp = false;

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

                if (swipeDelta.magnitude > minSwipeDistance)
                {
                    if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                    {
                        if (swipeDelta.x > 0) swipeRight = true;
                        else swipeLeft = true;
                    }
                    else
                    {
                        if (swipeDelta.y > 0) swipeUp = true;
                    }
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

    void Jump()
    {
        isJumping = true;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetBool("IsJumping", true);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }
    }

    void UpdateAnimations()
    {
        bool isMoving = rb.linearVelocity.magnitude > 0.05f && !isJumping;
        animator.SetBool("IsRunning", isMoving);
    }
}
