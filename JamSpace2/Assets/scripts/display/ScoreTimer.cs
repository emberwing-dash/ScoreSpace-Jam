using UnityEngine;
using TMPro;

public class ScoreTimer : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private float matchDuration = 300f;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Leaderboard")]
    [SerializeField] private GameObject leaderboardCanvas;

    private float currentTime;
    private bool matchEnded;

    private LeaderboardUI leaderboardUI;

    private void Awake()
    {
        leaderboardUI = GetComponent<LeaderboardUI>();

        // RESET timer EVERY time scene loads
        currentTime = matchDuration;
        matchEnded = false;
        Time.timeScale = 1f;

        if (leaderboardCanvas != null)
            leaderboardCanvas.SetActive(false);

        UpdateTimerUI();
    }

    private void Update()
    {
        if (matchEnded) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            EndMatch();
        }

        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        int m = Mathf.FloorToInt(currentTime / 60f);
        int s = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = $"{m:00}:{s:00}";
    }

    private void EndMatch()
    {
        matchEnded = true;

        if (leaderboardCanvas != null)
            leaderboardCanvas.SetActive(true);

        if (leaderboardUI != null)
            leaderboardUI.Refresh();

        Time.timeScale = 0f;
    }
}
