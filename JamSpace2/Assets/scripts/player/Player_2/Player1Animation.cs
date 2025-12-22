using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player1Animation : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Player1Movement movement;

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<Player1Movement>();
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
        // A → face left
        if (Input.GetKey(KeyCode.A))
        {
            spriteRenderer.flipX = true;
        }
        // D → face right
        else if (Input.GetKey(KeyCode.D))
        {
            spriteRenderer.flipX = false;
        }
    }

    // Animation Event
    public void EndAttack()
    {
        movement.EndAttack();

        animator.SetBool("isAttacking", false);
        animator.SetBool("isDribbling", false);
        animator.Play("Idle", 0, 0f);
    }
}
