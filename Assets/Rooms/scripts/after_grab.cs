using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; // Import XR Interaction Toolkit

public class AfterGrab : MonoBehaviour
{
    private AudioSource audioSource;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        
        if (grabInteractable == null)
        {
            Debug.LogError("XRGrabInteractable component not found, please make sure it is added to this game object");
            return;
        }
        
        // Register grab event
        grabInteractable.selectEntered.AddListener(OnGrab);
    }
    
    private void OnGrab(SelectEnterEventArgs args)
    {
        // Play audio when object is grabbed
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
            Debug.Log("Cat grabbed, playing audio");
        }
    }
    
    private void OnDestroy()
    {
        // Clean up event listener
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
        }
    }
}