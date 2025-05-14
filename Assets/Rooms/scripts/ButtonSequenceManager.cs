using UnityEngine;
using UnityEngine.UI;

public class SimpleButtonTrigger : MonoBehaviour
{
    [Header("Button Sequence")]
    public GameObject[] buttonSequence; // Define the correct button order in Inspector

    [Header("Continue Button")]
    public Button continueButton;

    private int currentStep = 0;

    private void Start()
    {
        if (continueButton != null)
        {
            continueButton.interactable = false;
        }
    }

    public void ButtonPressed(GameObject pressedButton)
    {
        if (buttonSequence == null || buttonSequence.Length == 0)
        {
            Debug.LogWarning("No button sequence defined.");
            return;
        }

        if (pressedButton == buttonSequence[currentStep])
        {
            Debug.Log($"Step {currentStep + 1}: Correct button pressed.");
            currentStep++;

            if (currentStep >= buttonSequence.Length)
            {
                Debug.Log("Sequence complete!");
                if (continueButton != null)
                {
                    continueButton.interactable = true;
                }
            }
        }
        else
        {
            Debug.Log("Wrong button or order. Resetting sequence.");
            currentStep = 0;

            if (continueButton != null)
            {
                continueButton.interactable = false;
            }
        }
    }
}
