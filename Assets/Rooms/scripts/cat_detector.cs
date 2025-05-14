using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CatDetector : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip meowClip;
    public AudioClip purrClip;

    [Header("Purr Settings")]
    public Transform targetObject;
    public float purrDistance = 1.5f;

    [Header("Haptics")]
    public float hapticIntensity = 0.3f;
    public float hapticDuration = 0.1f;

    private AudioSource audioSource;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInputInteractor currentInteractor = null;
    private bool isPurring = false;
    private float hapticTimer = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("Missing AudioSource component on Cat.");
            return;
        }

        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogError("Missing XRGrabInteractable on Cat.");
            return;
        }

        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void Update()
    {
        if (targetObject == null || audioSource == null || purrClip == null)
            return;

        float distance = Vector3.Distance(transform.position, targetObject.position);

        if (distance <= purrDistance)
        {
            if (!isPurring && !audioSource.isPlaying)
            {
                audioSource.clip = purrClip;
                audioSource.loop = true;
                audioSource.Play();
                isPurring = true;
                hapticTimer = 0f;
                Debug.Log("Cat is purring (close to target)");
            }

            if (isPurring && currentInteractor != null)
            {
                hapticTimer += Time.deltaTime;
                if (hapticTimer >= hapticDuration)
                {
                    currentInteractor.SendHapticImpulse(hapticIntensity, hapticDuration);
                    hapticTimer = 0f;
                }
            }
        }
        else
        {
            if (isPurring)
            {
                audioSource.Stop();
                audioSource.loop = false;
                isPurring = false;
                hapticTimer = 0f;
                Debug.Log("Cat stopped purring (too far from target)");
            }
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        currentInteractor = args.interactorObject as UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInputInteractor;

        if (meowClip != null)
        {
            audioSource.PlayOneShot(meowClip);
            Debug.Log("Cat grabbed, meow!");
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        currentInteractor = null;
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
            grabInteractable.selectExited.RemoveListener(OnRelease);
        }
    }
}
