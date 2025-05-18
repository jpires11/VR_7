using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ToggleSmoothTurnButton : MonoBehaviour
{
    public ControllerInputActionManager controllerInputActionManager; // assign in inspector

    public void ToggleSmoothTurn()
    {
        if (controllerInputActionManager != null)
        {
            controllerInputActionManager.smoothTurnEnabled = !controllerInputActionManager.smoothTurnEnabled;
            Debug.Log("Smooth Turn Enabled: " + controllerInputActionManager.smoothTurnEnabled);
        }
        else
        {
            Debug.LogWarning("ControllerInputActionManager reference is missing!");
        }
    }
}
