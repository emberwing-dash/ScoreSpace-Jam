using UnityEngine;

public class NameScoreList : MonoBehaviour
{
    [SerializeField] GameObject NameList;
    [SerializeField] GameObject ScoreList;
    [SerializeField] GameObject RankList;

    private void Start()
    {
        NameList.SetActive(false);
        ScoreList.SetActive(false);
        RankList.SetActive(false);
    }
    public void displayNameScore()
    {
        NameList.SetActive(true);
        ScoreList.SetActive(true);
        RankList .SetActive(true);
    }
}
