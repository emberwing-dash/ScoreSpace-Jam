using UnityEngine;
using TMPro;

public class ScoreTimer : MonoBehaviour
{
    [SerializeField] private float initialMatchTime = 300f;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject leaderboardCanvas;

    private static float matchTimeSeconds;
    private static bool timerInitialized;
    private static bool matchEnded;

    private LeaderboardUI leaderboardUI;

    private void Awake()
    {
        leaderboardUI = GetComponent<LeaderboardUI>();

        if (!timerInitialized)
        {
            matchTimeSeconds = initialMatchTime;
            timerInitialized = true;
        }
    }

    private void Start()
    {
        leaderboardCanvas.SetActive(false);
        UpdateTimerUI();
    }

    private void Update()
    {
        if (matchEnded) return;

        matchTimeSeconds -= Time.deltaTime;

        if (matchTimeSeconds <= 0f)
        {
            matchTimeSeconds = 0f;
            EndMatch();
        }

        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        int m = Mathf.FloorToInt(matchTimeSeconds / 60f);
        int s = Mathf.FloorToInt(matchTimeSeconds % 60f);
        timerText.text = $"{m:00}:{s:00}";
    }

    private void EndMatch()
    {
        matchEnded = true;

        leaderboardCanvas.SetActive(true);
        leaderboardUI.Refresh(); // 🔥 CRITICAL

        Time.timeScale = 0f;
    }
}
