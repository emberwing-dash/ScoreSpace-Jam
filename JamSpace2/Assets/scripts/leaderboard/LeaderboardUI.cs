using TMPro;
using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{
    [Header("Display")]
    [SerializeField] private TextMeshProUGUI[] leaderboardNameTexts;
    [SerializeField] private TextMeshProUGUI[] leaderboardScoreTexts;

    [SerializeField] private LeaderboardManager manager;

    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        string p1 = PlayerPrefs.GetString("Player1Name", "Player 1");
        string p2 = PlayerPrefs.GetString("Player2Name", "CPU");

        leaderboardNameTexts[0].text = p1;
        leaderboardScoreTexts[0].text = "Score: " + GameManager.score_1;

        leaderboardNameTexts[1].text = p2;
        leaderboardScoreTexts[1].text = "Score: " + GameManager.score_2;

        // Upload when match ends
        if (manager != null)
            manager.UploadMatchResults();
    }
}
