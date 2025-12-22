using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Canvas canva_1;
    [SerializeField] private Canvas canva_2;
    [SerializeField] private Canvas leaderboard;

    [Header("Enter Name Canvas")]
    [SerializeField] private Canvas name_1;
    [SerializeField] private Canvas name_2;

    void Start()
    {
        ShowMain();
    }

    void ShowMain()
    {
        canva_1.gameObject.SetActive(true);
        canva_2.gameObject.SetActive(false);
        leaderboard.gameObject.SetActive(false);
        name_1.gameObject.SetActive(false);
        name_2.gameObject.SetActive(false);
    }

    // ================= BUTTONS =================

    public void StartButton()
    {
        canva_1.gameObject.SetActive(false);
        canva_2.gameObject.SetActive(true);
        leaderboard.gameObject.SetActive(false);
        name_1.gameObject.SetActive(false);
        name_2.gameObject.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void BackButton()
    {
        ShowMain();
    }

    public void OnePlayer()
    {
        canva_1.gameObject.SetActive(false);
        canva_2.gameObject.SetActive(false);
        leaderboard.gameObject.SetActive(false);
        name_1.gameObject.SetActive(true);
        name_2.gameObject.SetActive(false);
    }

    public void TwoPlayer()
    {
        canva_1.gameObject.SetActive(false);
        canva_2.gameObject.SetActive(false);
        leaderboard.gameObject.SetActive(false);
        name_1.gameObject.SetActive(false);
        name_2.gameObject.SetActive(true);
    }
}
