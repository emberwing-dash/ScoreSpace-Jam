using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerNameInput : MonoBehaviour
{
    public InputField player1Input;
    public InputField player2Input;

    public void OnGoButtonClicked()
    {
        string player1Name = player1Input.text;
        string player2Name = player2Input.text;

        PlayerPrefs.SetString("Player1Name", player1Name);
        PlayerPrefs.SetString("Player2Name", player2Name);
        PlayerPrefs.Save();

        SceneManager.LoadScene("GameScene"); // next scene name
    }
}
