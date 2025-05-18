using UnityEngine;

public class VignetteToggleUI : MonoBehaviour
{
    [Tooltip("Drag your vignette tunneling GameObject here.")]
    public GameObject vignetteObject;

    public void ToggleVignette()
    {
        if (vignetteObject != null)
        {
            vignetteObject.SetActive(!vignetteObject.activeSelf);
        }
    }
}
