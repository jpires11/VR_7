using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HeadCrouchDetector : MonoBehaviour
{
    public Transform xrCamera;          // Assign your XR Rig's camera (head)
    public float crouchThreshold = 1.2f; // Meters ¡ª adjust based on your needs
    public Collider entranceCollider;   // The trigger collider at the room entrance

    private void OnTriggerStay(Collider other)
    {
        // Make sure the player is within the trigger zone
        if(other.transform == xrCamera.root) // Assuming XR camera is child of XR Rig
        {
            float headHeight = xrCamera.localPosition.y;

            if(headHeight < crouchThreshold)
            {
                Debug.Log("Player is crouching low enough to enter!");
                Debug.Log("Current position: " + headHeight);
                // Add logic: Open door, allow movement, disable wall collider, etc.
            }
            else
            {
                Debug.Log("Player is too high!");
                Debug.Log("Current position: " + headHeight);
            }
        }
    }
}
