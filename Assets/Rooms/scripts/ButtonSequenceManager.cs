using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimpleButtonTrigger : MonoBehaviour
{
    [Header("Button Sequence")]
    public GameObject[] buttonSequence; // Define the correct button order in Inspector

    [Header("Continue Button")]
    public Button continueButton;

    [Header("Audio Feedback")]
    public AudioSource audioSource;      // AudioSource to play the sounds
    public AudioClip winSound;           // Sound to play when correct button is pressed
    [Range(0f, 1f)] public float winSoundVolume = 1f;

    public AudioClip loseSound;          // Sound to play when incorrect button is pressed
    [Range(0f, 1f)] public float loseSoundVolume = 1f;

    public AudioClip completionSound;    // New sound to play after puzzle completion
    [Range(0f, 1f)] public float completionSoundVolume = 1f;

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
                audioSource.PlayOneShot(winSound, winSoundVolume);
            }

            currentStep++;

            if (currentStep >= buttonSequence.Length)
            {
                Debug.Log("Sequence complete!");

                if (continueButton != null)
                {
                    continueButton.interactable = true;
                }

                // Start coroutine to delay the completion sound
                if (audioSource != null && completionSound != null)
                {
                    StartCoroutine(PlayCompletionSoundDelayed(3f));
                }
            }
        }
        else
        {
            Debug.Log("Wrong button or order. Resetting sequence.");

            if (audioSource != null && loseSound != null)
            {
                audioSource.PlayOneShot(loseSound, loseSoundVolume);
            }

            currentStep = 0;

            if (continueButton != null)
            {
                continueButton.interactable = false;
            }
        }
    }

    private IEnumerator PlayCompletionSoundDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.PlayOneShot(completionSound, completionSoundVolume);
    }
}
