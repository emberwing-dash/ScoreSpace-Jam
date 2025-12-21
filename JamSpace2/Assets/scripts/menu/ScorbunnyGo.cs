using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScorbunnyGo : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 500f;
    public string nextSceneName = "Game";

    [Header("Required TMP Input Fields")]
    [SerializeField] private TMP_InputField[] requiredInputFields;

    private RectTransform rectTransform;
    private Animator animator;

    private bool isRunning = false;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        animator = GetComponent<Animator>();
    }

    // CALLED BY BUTTON
    public void StartRun()
    {
        // ✅ Validate TMP input fields
        if (!AreInputsValid())
        {
            Debug.Log("TMP Input fields are empty. Cannot start run.");
            return;
        }

        isRunning = true;

        // Play run animation
        if (animator != null)
            animator.SetBool("isRunning", true);

        // UI-safe flip (works under Button)
        rectTransform.localEulerAngles = new Vector3(0f, 180f, 0f);
    }

    void Update()
    {
        if (!isRunning) return;

        MoveRight();
        CheckOffScreen();
    }

    void MoveRight()
    {
        rectTransform.anchoredPosition += Vector2.right * moveSpeed * Time.deltaTime;
    }

    void CheckOffScreen()
    {
        float screenRight = Screen.width;

        float imageLeftEdge =
            rectTransform.position.x - rectTransform.rect.width * rectTransform.lossyScale.x;

        if (imageLeftEdge > screenRight)
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        isRunning = false;

        if (animator != null)
            animator.SetBool("isRunning", false);

        SceneManager.LoadScene(nextSceneName);
    }

    // 🔍 TMP INPUT VALIDATION
    bool AreInputsValid()
    {
        if (requiredInputFields == null || requiredInputFields.Length == 0)
            return true;

        foreach (TMP_InputField input in requiredInputFields)
        {
            if (input == null)
                return false;

            if (string.IsNullOrWhiteSpace(input.text))
                return false;
        }

        return true;
    }
}
