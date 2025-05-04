using UnityEngine;

public class SyncObject : MonoBehaviour
{
    public Transform targetObject; // Reference to the Room B object

    private Vector3 lastPosition;
    private Quaternion lastRotation;

    void Start()
    {
        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    void Update()
    {
        Vector3 positionDelta = transform.position - lastPosition;
        Quaternion rotationDelta = transform.rotation * Quaternion.Inverse(lastRotation);

        // Apply the deltas to the target
        if (targetObject != null)
        {
            targetObject.position += positionDelta;
            targetObject.rotation = rotationDelta * targetObject.rotation;
        }

        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }
}
