using UnityEngine;
using TMPro;

public class AntiCybersicknessToggleUI : MonoBehaviour
{
    [Tooltip("Drag your anti-cybersickness effect GameObject here.")]
    public GameObject antiCybersicknessObject;

    [Tooltip("Assign a TextMeshPro Text (TMP) object to display status.")]
    public TMP_Text statusText;

    public void ToggleAntiCybersickness()
    {
        if (antiCybersicknessObject != null)
        {
            bool isActive = !antiCybersicknessObject.activeSelf;
            antiCybersicknessObject.SetActive(isActive);

            UpdateStatusText(isActive);
        }
    }

    private void UpdateStatusText(bool isOn)
    {
        if (statusText != null)
        {
            statusText.text = isOn ? "Anti-Cybersickness ON" : "Anti-Cybersickness OFF";
        }
    }

    // Optional: initialize status text on start
    private void Start()
    {
        if (antiCybersicknessObject != null)
        {
            UpdateStatusText(antiCybersicknessObject.activeSelf);
        }
    }
}
