using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CatDetector : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip meowClip;
    public AudioClip purrClip;

    [Header("Purr Settings")]
    public Transform targetObject; // Object to detect proximity to
    public float purrDistance = 1.5f;

    private AudioSource audioSource;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private bool isPurring = false;

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
                Debug.Log("Cat is purring (close to target)");
            }
        }
        else
        {
            if (isPurring)
            {
                audioSource.Stop();
                audioSource.loop = false;
                isPurring = false;
                Debug.Log("Cat stopped purring (too far from target)");
            }
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (meowClip != null && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(meowClip);
            Debug.Log("Cat grabbed, meow!");
        }
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
        }
    }
}
