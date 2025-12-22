using UnityEngine;
using Dan.Main;
using TMPro;
using System.Collections.Generic;

public class LeaderboardManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private List<TextMeshProUGUI> names;
    [SerializeField] private List<TextMeshProUGUI> scores;

    [Header("Leaderboard Key")]
    [SerializeField]
    private string key =
        "f8def089e0377edbb507ab6312a10627475fd8a98ad898d01141071fafbbb550";

    private void Start()
    {
        GetLeaderboard();
    }

    // ================= GET ONLINE LEADERBOARD =================
    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(key, msg =>
        {
            int loopLength = Mathf.Min(msg.Length, names.Count);

            for (int i = 0; i < loopLength; i++)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        });
    }

    // ================= UPLOAD MATCH RESULTS =================
    public void UploadMatchResults()
    {
        UploadPlayer(0, GameManager.score_1);
        UploadPlayer(1, GameManager.score_2);
    }

    private void UploadPlayer(int index, int score)
    {
        string playerName = PlayerPrefs.GetString(
            index == 0 ? "Player1Name" : "Player2Name",
            index == 0 ? "Player 1" : "CPU"
        );

        // ❌ Skip CPU
        if (playerName == "CPU") return;

        // 🔥 FORCE UNIQUE USER PER DEVICE (PvP FIX)
        string uniqueName = playerName;

        LeaderboardCreator.UploadNewEntry(
            key,
            uniqueName,
            score,
            success =>
            {
                if (success)
                    GetLeaderboard();
            }
        );
    }
}
