using UnityEngine;

public class UIAudioManager : MonoBehaviour
{
    public static UIAudioManager Instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clickSound;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    public void PlayClick()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(clickSound);
    }
}
