using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUI : MonoBehaviour
{
    public Text player1Text;
    public Text player2Text;

    void Start()
    {
        player1Text.text = PlayerPrefs.GetString("Player1Name", "Player 1");
        player2Text.text = PlayerPrefs.GetString("Player2Name", "Player 2");
    }
}
