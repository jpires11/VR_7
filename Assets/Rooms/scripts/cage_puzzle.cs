﻿using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using System.Collections;

public class CagePuzzle : MonoBehaviour
{
    public CustomSocketInteraction1 socket1;
    public CustomSocketInteraction2 socket2;
    public CustomSocketInteraction3 socket3;

    public Button continueButton;

    [Header("Audio")]
    public AudioSource audioSource;          // Assign in Inspector
    public AudioClip taskCompleteSound;      // Sound to play when puzzle is complete

    private bool isDoorOpen = false;

    private void Start()
    {
        if (continueButton != null)
        {
            continueButton.interactable = false;
        }

        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource is not assigned!");
        }
    }

    private void Update()
    {
        if (!isDoorOpen && socket1 != null && socket2 != null && socket3 != null &&
            socket1.isSocketed && socket2.isSocketed && socket3.isSocketed)
        {
            Debug.LogWarning("Yes! the cage will open!");
            OpenDoor();
            isDoorOpen = true;
        }
    }

    private void OpenDoor()
    {
        Debug.Log("Door is opening...");
        StartCoroutine(OpenDoorWithDelay());
    }

    private IEnumerator OpenDoorWithDelay()
    {
        yield return new WaitForSeconds(2);

        Animator animator = GetComponent<Animator>();
        GameObject freeRoomA = GameObject.Find("Free_RoomA");
        GameObject kittyObject = null;

        if (freeRoomA != null)
        {
            Transform kittyTransform = freeRoomA.transform.Find("Kitty_001");
            if (kittyTransform != null)
            {
                kittyObject = kittyTransform.gameObject;
            }
            else
            {
                Debug.LogWarning("Can't find Kitty_001 in Free_RoomA!");
            }
        }
        else
        {
            Debug.LogWarning("Can't find Free_RoomA object!");
        }

        if (animator != null && kittyObject != null)
        {
            animator.SetTrigger("open_cage");
            Debug.LogWarning("trigger open_cage!");

            Animator kittyAnimator = kittyObject.GetComponent<Animator>();
            if (kittyAnimator != null)
            {
                kittyAnimator.SetTrigger("open_cage");
                KittyFollowPlayer followScript = kittyObject.GetComponent<KittyFollowPlayer>();
                if (followScript != null)
                {
                    followScript.StartFollowing();
                }
                Debug.LogWarning("trigger open_cage on Kitty_001!");
            }
        }
        else
        {
            Debug.LogWarning("Animator component not found or Kitty_001 not found!");
        }

        // ✅ Play the completion sound
        if (audioSource != null && taskCompleteSound != null)
        {
            audioSource.PlayOneShot(taskCompleteSound);
        }
        else
        {
            Debug.LogWarning("AudioSource or TaskCompleteSound not assigned!");
        }

        // ✅ Enable continue button
        if (continueButton != null)
        {
            continueButton.interactable = true;
            Debug.Log("Continue button is now interactable!");
        }
        else
        {
            Debug.LogWarning("Continue button reference is missing!");
        }
    }
}
