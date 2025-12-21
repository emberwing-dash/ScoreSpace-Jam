using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Debug.Log("GOAL!!");

            SoccerPush soccerPush = collision.GetComponent<SoccerPush>();
            if (soccerPush != null && soccerPush.isPlayerTouchingBall)
            {
                // increase player score
                GameManager.score_1 += 1;
            }
            else
            {
                // increase cpu score
                GameManager.score_2+= 1;
            }

            //after score, reload scene using Scene Management and keep the score record saved
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
