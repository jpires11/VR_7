using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ToggleSmoothTurnButton : MonoBehaviour
{
    [Tooltip("Assign your ControllerInputActionManager here.")]
    public ControllerInputActionManager controllerInputActionManager; // assign in inspector

    [Tooltip("Assign a TextMeshPro UI text to show the current rotation mode.")]
    public TMP_Text rotationModeText;

    public void ToggleSmoothTurn()
    {
        if (controllerInputActionManager != null)
        {
            controllerInputActionManager.smoothTurnEnabled = !controllerInputActionManager.smoothTurnEnabled;
            Debug.Log("Smooth Turn Enabled: " + controllerInputActionManager.smoothTurnEnabled);

            UpdateRotationModeText(controllerInputActionManager.smoothTurnEnabled);
        }
        else
        {
            Debug.LogWarning("ControllerInputActionManager reference is missing!");
        }
    }

    private void UpdateRotationModeText(bool smoothTurnEnabled)
    {
        if (rotationModeText != null)
        {
            rotationModeText.text = smoothTurnEnabled ? "Continuous Rotation" : "Snap Rotation";
        }
    }

    // Optional: initialize the text on Start to reflect current mode
    private void Start()
    {
        if (controllerInputActionManager != null)
        {
            UpdateRotationModeText(controllerInputActionManager.smoothTurnEnabled);
        }
    }
}
