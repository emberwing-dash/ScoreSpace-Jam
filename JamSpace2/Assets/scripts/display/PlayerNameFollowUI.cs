using UnityEngine;
using TMPro;

public class PlayerNameFollowUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform[] players;         // Player world objects
    [SerializeField] private TextMeshProUGUI[] nameTexts; // UI name texts

    [Header("Positioning (per player)")]
    [SerializeField] private Vector3[] worldOffsets;      // Offset per player

    private Camera mainCam;

    void Awake()
    {
        mainCam = Camera.main;

        // Set names from PlayerPrefs
        if (nameTexts.Length > 0)
            nameTexts[0].text = PlayerPrefs.GetString("Player1Name", "Player 1");

        if (nameTexts.Length > 1)
            nameTexts[1].text = PlayerPrefs.GetString("Player2Name", "Player 2");
    }

    void LateUpdate()
    {
        if (players == null || nameTexts == null || worldOffsets == null) return;

        int count = Mathf.Min(players.Length, nameTexts.Length, worldOffsets.Length);

        for (int i = 0; i < count; i++)
        {
            if (players[i] == null || nameTexts[i] == null) continue;

            Vector3 worldPos = players[i].position + worldOffsets[i];
            Vector3 screenPos = mainCam.WorldToScreenPoint(worldPos);

            nameTexts[i].transform.position = screenPos;
        }
    }


    public string GetName(int index)
    {
        if (index < 0 || index >= nameTexts.Length) return "";
        return nameTexts[index].text;
    }

}
