using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float sprintSpeed = 8f;

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
        ReadInput();
        UpdateState();
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    void ReadInput()
    {
        isSprinting = Input.GetKey(KeyCode.LeftShift);

        MovementInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;
    }

    void UpdateState()
    {
        CurrentState = MovementInput == Vector2.zero
            ? PlayerState.Idle
            : PlayerState.Dribble;
    }

    void ApplyMovement()
    {
        float currentSpeed = isSprinting ? sprintSpeed : speed;
        rb.linearVelocity = MovementInput * currentSpeed;
    }
}
