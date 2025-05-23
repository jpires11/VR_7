using UnityEngine;

public class SlidingBridge : MonoBehaviour
{
    [Tooltip("The local Z offset when the door is fully open")]
    public float openZPosition = 2.0f;

    [Tooltip("The starting local Z position (closed state)")]
    public float closedZPosition = 0.0f;

    [Tooltip("The Transform to move (defaults to this transform)")]
    public Transform doorTransform;

    void Awake()
    {
        if (doorTransform == null)
            doorTransform = transform;
    }

    /// <summary>
    /// Sets the door's vertical position based on knob value (0¨C1)
    /// </summary>
    /// <param name="value">Normalized value from the knob (0 = closed, 1 = open)</param>
    public void SetDoorOpenAmount(float value)
    {
        var z = Mathf.Lerp(closedZPosition, openZPosition, value);
        var pos = doorTransform.localPosition;
        pos.z = z;
        doorTransform.localPosition = pos;
    }
}
