using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerNameInput : MonoBehaviour
{
    [Header("Player Name Inputs (Index 0 = Player1, Index 1 = Player2)")]
    [SerializeField] private TMP_InputField[] playerInputs;

    // ✅ Version 1: Player vs Player
    public void OnGoButtonClicked_PvP()
    {
        string player1Name = playerInputs[0].text;
        string player2Name = playerInputs[1].text;

        PlayerPrefs.SetString("Player1Name", player1Name);
        PlayerPrefs.SetString("Player2Name", player2Name);
        PlayerPrefs.Save();

        
    }

    // ✅ Version 2: Player vs CPU
    public void OnGoButtonClicked_CPU()
    {
        string player1Name = playerInputs[0].text;

        PlayerPrefs.SetString("Player1Name", player1Name);
        PlayerPrefs.SetString("Player2Name", "CPU");
        PlayerPrefs.Save();

        
    }
}
