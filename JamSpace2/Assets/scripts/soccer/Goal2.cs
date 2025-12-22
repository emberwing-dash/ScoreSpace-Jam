using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal2 : MonoBehaviour
{
    [SerializeField] private Transform ball;
    [SerializeField] private Transform ballSpawn;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            if (!collision.CompareTag("Ball")) return;

            Debug.Log("GOAL!!");

            // Increase score
            GameManager.score_1 += 1;

            ResetBall();
        }
    }

    private void ResetBall()
    {
        ball.position = ballSpawn.position;

        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}
