using UnityEngine;
using UnityEngine.UI;

public class SimpleButtonTrigger : MonoBehaviour
{
    [Header("Button Sequence")]
    public GameObject[] buttonSequence; // Define the correct button order in Inspector

    [Header("Continue Button")]
    public Button continueButton;

    [Header("Audio Feedback")]
    public AudioSource audioSource; // AudioSource to play the sounds
    public AudioClip winSound;      // Sound to play when correct button is pressed
    public AudioClip loseSound;     // Sound to play when incorrect button is pressed

    private int currentStep = 0;

    private void Start()
    {
        if (continueButton != null)
        {
            continueButton.interactable = false;
        }

        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource is not assigned in the Inspector.");
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

            if (audioSource != null && winSound != null)
            {
                audioSource.PlayOneShot(winSound);
            }

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

            if (audioSource != null && loseSound != null)
            {
                audioSource.PlayOneShot(loseSound);
            }

            currentStep = 0;

            if (continueButton != null)
            {
                continueButton.interactable = false;
            }
        }
    }
}
