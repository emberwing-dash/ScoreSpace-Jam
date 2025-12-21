using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int score_1;
    public static int score_2;

    [SerializeField] TextMeshProUGUI text_1;
    [SerializeField] TextMeshProUGUI text_2;

    private void Update()
    {
        text_1.text = "Score: "+score_1.ToString();
        text_2.text = "Score: "+score_2.ToString();
    }
}
