using UnityEngine;

public class SoccerPush : MonoBehaviour
{
    public bool isPlayerTouchingBall;

    [Header("Audio")]
    [SerializeField] private AudioSource kickAudioSource;
    [SerializeField] private AudioSource bgAudioSource;
    [Range(0f, 1f)]
    [SerializeField] private float kickVolume = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float bgDuckVolume = 0.2f;
    [SerializeField] private float duckDuration = 0.5f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (kickAudioSource == null) Debug.LogWarning("Kick AudioSource not assigned", this);
        if (bgAudioSource == null) Debug.LogWarning("BG AudioSource not assigned", this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerTouchingBall = true;

            if (kickAudioSource != null)
            {
                kickAudioSource.volume = kickVolume;
                kickAudioSource.Play();

                // Duck background audio
                if (bgAudioSource != null)
                    StartCoroutine(DuckBackground());
            }
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            isPlayerTouchingBall = false;
        }

        Vector2 dir = (transform.position - collision.transform.position).normalized;
        rb.AddForce(dir * 2f, ForceMode2D.Impulse);
    }

    private System.Collections.IEnumerator DuckBackground()
    {
        float originalVolume = bgAudioSource.volume;
        bgAudioSource.volume = bgDuckVolume;

        yield return new WaitForSeconds(duckDuration);

        bgAudioSource.volume = originalVolume;
    }
}
