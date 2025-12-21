using UnityEngine;

public class Player1Movement : MonoBehaviour
{
    public float speed = 5f;
    public float sprintMultiplier = 1.5f;

    public PlayerState CurrentState { get; private set; } = PlayerState.Idle;
    public Vector2 MovementInput { get; private set; }

    private Rigidbody2D rb;

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
        // Attack input
        if (Input.GetKeyDown(KeyCode.Space) && CurrentState != PlayerState.Attack)
        {
            CurrentState = PlayerState.Attack;
            MovementInput = Vector2.zero;
            return;
        }

        // WASD input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        MovementInput = new Vector2(moveX, moveY).normalized;

        // Sprint
        if (Input.GetKey(KeyCode.LeftShift))
            MovementInput *= sprintMultiplier;
    }

    void UpdateState()
    {
        if (CurrentState == PlayerState.Attack)
            return;

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

    public void EndAttack()
    {
        CurrentState = PlayerState.Idle;
    }
}
