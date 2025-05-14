using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    [Tooltip("The local Y offset when the door is fully open")]
    public float openHeight = 2.0f;

    [Tooltip("The starting local Y position (closed state)")]
    public float closedYPosition = 0.0f;

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
        var y = Mathf.Lerp(closedYPosition, openHeight, value);
        var pos = doorTransform.localPosition;
        pos.y = y;
        doorTransform.localPosition = pos;
    }
}
