using UnityEngine;

public class SoccerPush : MonoBehaviour
{
    public bool isPlayerTouchingBall;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Player touches ball → TRUE
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerTouchingBall = true;

           
        }

        // Enemy touches ball → FALSE
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            isPlayerTouchingBall = false;
        }

        Vector2 dir =
               (transform.position - collision.transform.position).normalized;

        GetComponent<Rigidbody2D>().AddForce(dir * 2f);
    }
}
