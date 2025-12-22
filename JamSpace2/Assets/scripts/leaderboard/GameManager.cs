using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int score_1;
    public static int score_2;

    [SerializeField] private TextMeshProUGUI text_1;
    [SerializeField] private TextMeshProUGUI text_2;

    private void Update()
    {
        text_1.text = "Score: " + score_1;
        text_2.text = "Score: " + score_2;
    }

    // Optional helpers
    public static void AddScorePlayer1() => score_1++;
    public static void AddScorePlayer2() => score_2++;
}
