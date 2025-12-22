using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement movement;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponentInParent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        UpdateAnimator();
        UpdateFlip();
    }

    void UpdateAnimator()
    {
        // Only Idle / Dribble
        animator.SetBool("isDribbling", movement.CurrentState == PlayerState.Dribble);

        if (movement.MovementInput != Vector2.zero)
        {
            animator.SetFloat("moveX", movement.MovementInput.x);
            animator.SetFloat("moveY", movement.MovementInput.y);
        }
    }

    void UpdateFlip()
    {
        // Face left
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || movement.MovementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        // Face right
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || movement.MovementInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}
