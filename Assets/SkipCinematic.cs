using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class SkipCinematic : MonoBehaviour
{
    public InputActionReference skipAction;
    public float holdDuration = 3f;
    public UnityEvent skipToScene;

    private float holdTimer = 0f;
    private bool hasTriggered = false;

    void Update()
    {
        if (hasTriggered) return;

        if (skipAction.action.ReadValue<float>() > 0.5f)
        {
            holdTimer += Time.deltaTime;

            if (holdTimer >= holdDuration)
            {
                hasTriggered = true;
                skipToScene.Invoke();
            }
        }
        else
        {
            holdTimer = 0f;
        }
    }
}
