using UnityEngine;

public class UIButtonClickSound : MonoBehaviour
{
    [Header("Assign the AudioSource from the separate GameObject")]
    [SerializeField] private AudioSource audioSource;

    void Awake()
    {
        if (audioSource == null)
            Debug.LogError("AudioSource is not assigned in UIButtonClickSound!");
    }

    // Call this from the Button OnClick
    public void PlayClick()
    {
        if (audioSource != null)
            audioSource.Play(); // just play the clip
    }
}
