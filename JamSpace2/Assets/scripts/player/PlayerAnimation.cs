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
        spriteRenderer = GetComponent<SpriteRenderer>(); // or GetComponentInChildren if needed
    }

    void Update()
    {
        UpdateAnimator();
        UpdateFlip();
    }

    void UpdateAnimator()
    {
        animator.SetBool("isDribbling", movement.CurrentState == PlayerState.Dribble);
        animator.SetBool("isAttacking", movement.CurrentState == PlayerState.Attack);

        if (movement.MovementInput != Vector2.zero)
        {
            animator.SetFloat("moveX", movement.MovementInput.x);
            animator.SetFloat("moveY", movement.MovementInput.y);
        }
    }

    void UpdateFlip()
    {
        // Flip when Left Arrow or A is pressed (even diagonally)
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

    // Animation Event → last frame of attack
    public void EndAttack()
    {
        movement.EndAttack();

        // FORCE animator back to Idle
        animator.SetBool("isAttacking", false);
        animator.SetBool("isDribbling", false);

        animator.Play("Idle", 0, 0f);
    }

}
