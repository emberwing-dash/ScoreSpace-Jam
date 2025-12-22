using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Debug.Log("GOAL!!");
            GameManager.score_2 += 1;
        }

        

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
