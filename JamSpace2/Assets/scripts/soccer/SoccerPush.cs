using UnityEngine;

public class SoccerPush : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 dir =
                (transform.position - collision.transform.position).normalized;

            GetComponent<Rigidbody2D>()
                .AddForce(dir * 2f);
        }
    }

}
