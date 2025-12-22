using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class RotateButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private bool rotateClockwise = true;

    [Header("Hover Scale")]
    [SerializeField] private float hoverScale = 1.15f;
    [SerializeField] private float scaleSpeed = 8f;

    private RectTransform rectTransform;
    private Vector3 originalScale;
    private Vector3 targetScale;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
        targetScale = originalScale;
    }

    private void Update()
    {
        // Rotate
        float direction = rotateClockwise ? -1f : 1f;
        rectTransform.Rotate(0f, 0f, direction * rotationSpeed * Time.unscaledDeltaTime);

        // Smooth scale
        rectTransform.localScale = Vector3.Lerp(
            rectTransform.localScale,
            targetScale,
            Time.unscaledDeltaTime * scaleSpeed
        );
    }

    // 🖱 Hover enter
    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * hoverScale;
    }

    // 🖱 Hover exit
    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
    }

    // 🔘 Button OnClick → Load Menu Scene
    public void LoadMenuScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
