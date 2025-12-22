using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAnimationController : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private float moveThreshold = 0.05f;

    private Animator animator;
    private Rigidbody2D rb;

    private float initialScaleX;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // ✅ Store initial facing direction
        initialScaleX = Mathf.Sign(transform.localScale.x);
        if (initialScaleX == 0) initialScaleX = 1f;
    }

    void Update()
    {
        UpdateAnimation();
        UpdateFlip();
    }

    void UpdateAnimation()
    {
        bool isMoving = rb.linearVelocity.magnitude > moveThreshold;
        animator.SetBool("isRunning", isMoving);
    }

    void UpdateFlip()
    {
        float vx = rb.linearVelocity.x;

        if (vx > 0.05f)
        {
            // Move right → face right relative to initial orientation
            transform.localScale = new Vector3(
                Mathf.Abs(initialScaleX),
                transform.localScale.y,
                transform.localScale.z
            );
        }
        else if (vx < -0.05f)
        {
            // Move left → face left relative to initial orientation
            transform.localScale = new Vector3(
                -Mathf.Abs(initialScaleX),
                transform.localScale.y,
                transform.localScale.z
            );
        }
    }
}
