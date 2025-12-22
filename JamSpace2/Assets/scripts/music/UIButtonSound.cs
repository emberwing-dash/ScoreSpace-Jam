using UnityEngine;

public class UIButtonClick : MonoBehaviour
{
    public void OnClick()
    {
        UIAudioManager.Instance.PlayClick();

        // NOW it's safe to disable UI / canvas
        gameObject.SetActive(false);
    }
}
