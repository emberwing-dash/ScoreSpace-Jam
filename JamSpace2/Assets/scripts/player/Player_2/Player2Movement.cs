using UnityEngine;

public class Player2Movement : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.Keypad0) && CurrentState != PlayerState.Attack)
        {
            CurrentState = PlayerState.Attack;
            MovementInput = Vector2.zero;
            return;
        }

        // Arrow keys input for Player2
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.LeftArrow)) moveX = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) moveX = 1f;
        if (Input.GetKey(KeyCode.UpArrow)) moveY = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) moveY = -1f;

        // Normalize to avoid faster diagonal movement
        MovementInput = new Vector2(moveX, moveY).normalized;

        // Sprint with Right Shift
        if (Input.GetKey(KeyCode.RightShift))
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
