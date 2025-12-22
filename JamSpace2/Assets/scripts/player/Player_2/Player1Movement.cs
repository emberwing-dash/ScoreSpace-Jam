using UnityEngine;

public class Player1Movement : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CurrentState = PlayerState.Attack;
            MovementInput = Vector2.zero;
            return;
        }

        float x = 0f;
        float y = 0f;

        if (Input.GetKey(KeyCode.A)) x = -1f;
        if (Input.GetKey(KeyCode.D)) x = 1f;
        if (Input.GetKey(KeyCode.W)) y = 1f;
        if (Input.GetKey(KeyCode.S)) y = -1f;

        MovementInput = new Vector2(x, y).normalized;

        // ✅ Left Shift sprint
        isSprinting = Input.GetKey(KeyCode.LeftShift);
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
