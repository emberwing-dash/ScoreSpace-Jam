using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation2 : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Player1Movement player1Movement;
    private Player2Movement player2Movement;

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Try to get Player1Movement first
        player1Movement = GetComponentInParent<Player1Movement>();
        player2Movement = GetComponentInParent<Player2Movement>();
    }

    void Update()
    {
        UpdateAnimator();
        UpdateFlip();
    }

    void UpdateAnimator()
    {
        if (player1Movement != null)
        {
            animator.SetBool("isDribbling", player1Movement.CurrentState == PlayerState.Dribble);
            animator.SetBool("isAttacking", player1Movement.CurrentState == PlayerState.Attack);

            if (player1Movement.MovementInput != Vector2.zero)
            {
                animator.SetFloat("moveX", player1Movement.MovementInput.x);
                animator.SetFloat("moveY", player1Movement.MovementInput.y);
            }
        }
        else if (player2Movement != null)
        {
            animator.SetBool("isDribbling", player2Movement.CurrentState == PlayerState.Dribble);
            animator.SetBool("isAttacking", player2Movement.CurrentState == PlayerState.Attack);

            if (player2Movement.MovementInput != Vector2.zero)
            {
                animator.SetFloat("moveX", player2Movement.MovementInput.x);
                animator.SetFloat("moveY", player2Movement.MovementInput.y);
            }
        }
    }

    void UpdateFlip()
    {
        if (player1Movement != null)
        {
            if (player1Movement.MovementInput.x < 0) spriteRenderer.flipX = true;
            else if (player1Movement.MovementInput.x > 0) spriteRenderer.flipX = false;
        }
        else if (player2Movement != null)
        {
            if (player2Movement.MovementInput.x < 0) spriteRenderer.flipX = true;
            else if (player2Movement.MovementInput.x > 0) spriteRenderer.flipX = false;
        }
    }

    // Called by animation event at the end of attack
    public void EndAttack()
    {
        if (player1Movement != null)
        {
            player1Movement.EndAttack();
        }
        else if (player2Movement != null)
        {
            player2Movement.EndAttack();
        }

        // Reset animator
        animator.SetBool("isAttacking", false);
        animator.SetBool("isDribbling", false);
        animator.Play("Idle", 0, 0f);
    }
}
