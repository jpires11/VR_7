using UnityEngine;
using UnityEngine.UI;

public class LevelCompletion : MonoBehaviour
{
    [Header("Continue Button")]
    public Button continueButton;

    void Start()
    {
        // Ensure the button is non-interactable at the start
        if (continueButton != null)
            continueButton.interactable = false;
    }

    // Call this when the level or puzzle is completed
    public void OnLevelCompleted()
    {
        Debug.Log("Level completed. Enabling continue button.");

        if (continueButton != null)
            continueButton.interactable = true;
    }
}
