using UnityEngine;
using Unity.XR.CoreUtils; // Required if using XROrigin

public class CatModelFollower : MonoBehaviour
{
    private Transform playerHead;
    public float yOffset = -1.75f; // Approximate vertical offset to feet level
    public Vector3 positionOffset = Vector3.zero; // Optional for fine-tuning
    public bool matchRotation = false; // Optional: match player yaw

    void Start()
    {
        // Try to find the player's camera (head position)
        if (Camera.main != null)
        {
            playerHead = Camera.main.transform;
        }
        else
        {
            var xrOrigin = FindObjectOfType<XROrigin>();
            if (xrOrigin != null)
            {
                playerHead = xrOrigin.Camera.transform;
            }
        }

        if (playerHead == null)
        {
            Debug.LogError("FootFollower: Could not find player head or camera.");
        }
    }

    void LateUpdate()
    {
        if (playerHead == null) return;

        // Set cube position to be under the player¡¯s head (approx feet)
        Vector3 newPosition = playerHead.position + Vector3.up * yOffset + positionOffset;
        transform.position = newPosition;

        if (matchRotation)
        {
            transform.rotation = Quaternion.Euler(0, playerHead.rotation.eulerAngles.y, 0);
        }
    }
}
