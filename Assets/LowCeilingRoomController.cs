using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowCeilingRoomController : MonoBehaviour
{
    public Transform xrCamera;               // The headset (Camera inside XR Rig)
    public float crouchThreshold = 1.2f;     // Max allowed height in the room
    public GameObject playerBody;            // The XR Rig or movement controller
    public Canvas warningUI;                 // (Optional) UI warning to crouch

    private bool isInLowRoom = false;

    void Update()
    {
        if (isInLowRoom)
        {
            float headHeight = xrCamera.localPosition.y;
            // Show debug height
            Debug.Log("Head height: " + headHeight.ToString("F2"));

            if (headHeight > crouchThreshold)
            {
                // Too tall ¡ú freeze movement
                FreezePlayer();
                if (warningUI) warningUI.enabled = true;
            }
            else
            {
                // Crouched ¡ú allow movement
                UnfreezePlayer();
                if (warningUI) warningUI.enabled = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == xrCamera.root)
        {
            isInLowRoom = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == xrCamera.root)
        {
            isInLowRoom = false;
            UnfreezePlayer();
            if (warningUI) warningUI.enabled = false;
        }
    }

    void FreezePlayer()
    {
        // Disable movement ¡ª depends on your locomotion system!
        playerBody.GetComponent<CharacterController>().enabled = false;
    }

    void UnfreezePlayer()
    {
        // Re-enable movement
        playerBody.GetComponent<CharacterController>().enabled = true;
    }
}
