using UnityEngine;

public class Player2Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float sprintMultiplier = 1.5f;

    public PlayerState CurrentState { get; private set; } = PlayerState.Idle;
    public Vector2 MovementInput { get; private set; }

    private Rigidbody2D rb;
    private bool isSprinting;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        // Attack
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            CurrentState = PlayerState.Attack;
            MovementInput = Vector2.zero;
            return;
        }

        float x = 0f;
        float y = 0f;

        if (Input.GetKey(KeyCode.LeftArrow)) x = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) x = 1f;
        if (Input.GetKey(KeyCode.UpArrow)) y = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) y = -1f;

        MovementInput = new Vector2(x, y).normalized;

        // ✅ Right Shift sprint
        isSprinting = Input.GetKey(KeyCode.RightShift);
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

        float finalSpeed = isSprinting ? speed * sprintMultiplier : speed;
        rb.linearVelocity = MovementInput * finalSpeed;
    }

    public void EndAttack()
    {
        CurrentState = PlayerState.Idle;
    }
}
