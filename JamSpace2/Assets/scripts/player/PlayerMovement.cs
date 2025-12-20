using UnityEngine;

public enum PlayerState
{
    Idle,
    Dribble,
    Attack
}


public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    public PlayerState CurrentState { get; private set; } = PlayerState.Idle;
    public Vector2 MovementInput { get; private set; }

    private Rigidbody2D rb;

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
        // Attack input
        if (Input.GetKeyDown(KeyCode.Space) && CurrentState != PlayerState.Attack)
        {
            CurrentState = PlayerState.Attack;
            MovementInput = Vector2.zero;
            return;
        }

        MovementInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;
    }

    void UpdateState()
    {
        if (CurrentState == PlayerState.Attack)
            return;

        if (MovementInput == Vector2.zero)
            CurrentState = PlayerState.Idle;
        else
            CurrentState = PlayerState.Dribble;
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
