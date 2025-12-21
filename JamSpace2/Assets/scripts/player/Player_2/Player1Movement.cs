using UnityEngine;

public class Player1Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float sprintMultiplier = 1.5f;

    public PlayerState CurrentState { get; private set; } = PlayerState.Idle;
    public Vector2 MovementInput { get; private set; }

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
            Debug.LogError($"{gameObject.name} is missing Rigidbody2D!");
    }

    void Update()
    {
        if (CurrentState != PlayerState.Attack)
        {
            ReadInput();
            UpdateState();
        }
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    void ReadInput()
    {
        // Attack input
        if (Input.GetKeyDown(KeyCode.Space) && CurrentState != PlayerState.Attack)
        {
            CurrentState = PlayerState.Attack;
            MovementInput = Vector2.zero;
            return;
        }

        // WASD input (only)
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;
        if (Input.GetKey(KeyCode.W)) moveY = 1f;
        if (Input.GetKey(KeyCode.S)) moveY = -1f;

        // Normalize for diagonal movement
        MovementInput = new Vector2(moveX, moveY).normalized;

        // Sprint with Left Shift
        if (Input.GetKey(KeyCode.LeftShift))
            MovementInput *= sprintMultiplier;
    }

    void UpdateState()
    {
        if (CurrentState == PlayerState.Attack) return;

        CurrentState = MovementInput == Vector2.zero ? PlayerState.Idle : PlayerState.Dribble;
    }

    void ApplyMovement()
    {
        if (CurrentState == PlayerState.Attack)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = MovementInput * speed;
    }

    // Called by animation event
    public void EndAttack()
    {
        CurrentState = PlayerState.Idle;
    }
}
