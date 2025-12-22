using TMPro;
using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{
    [Header("Leaderboard Display (Top → Bottom)")]
    [SerializeField] private TextMeshProUGUI[] leaderboardNameTexts;
    [SerializeField] private TextMeshProUGUI[] leaderboardScoreTexts;

    [Header("Source")]
    [SerializeField] private PlayerNameFollowUI playerNameSource;

    public void Refresh()
    {
        string[] names = new string[2];
        int[] scores = new int[2];

        names[0] = playerNameSource.GetName(0);
        names[1] = playerNameSource.GetName(1);

        scores[0] = GameManager.score_1;
        scores[1] = GameManager.score_2;

        int top = scores[0] >= scores[1] ? 0 : 1;
        int bottom = top == 0 ? 1 : 0;

        leaderboardNameTexts[0].text = names[top];
        leaderboardScoreTexts[0].text = "Score: " + scores[top];

        leaderboardNameTexts[1].text = names[bottom];
        leaderboardScoreTexts[1].text = "Score: " + scores[bottom];
    }
}
