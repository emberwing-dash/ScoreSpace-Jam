using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] Canvas canva_1;
    [SerializeField] Canvas canva_2;
    [SerializeField] Canvas leaderboard;

    [Header("Enter Name Canvas")]
    [SerializeField] Canvas name_1;
    [SerializeField] Canvas name_2;

    void Start()
    {
        canva_1.gameObject.SetActive(true);
        canva_2.gameObject.SetActive(false);
        leaderboard.gameObject.SetActive(false);

        name_1.gameObject.SetActive(false);
        name_2.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        canva_1.gameObject.SetActive(true);
        canva_2.gameObject.SetActive(false);
        leaderboard.gameObject.SetActive(false);

        name_1.gameObject.SetActive(false);
        name_2.gameObject.SetActive(false);
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
