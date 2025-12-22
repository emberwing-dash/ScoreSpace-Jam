using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    [SerializeField] Canvas leaderboardCanva;

    private void Start()
    {
        leaderboardCanva.gameObject.SetActive(false);
    }
    public void displayLeaderboard()
    {
        leaderboardCanva.gameObject.SetActive(true);
    }
}
