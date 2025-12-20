using UnityEngine;

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
            }
            else
            {
                // increase cpu score
            }

            //after score, reload scene using Scene Management and keep the score record saved
        }
    }
}
