using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable))]
public class InteractionManagerTest : MonoBehaviour
{
    public string targetTag = "Player";
    public UnityEvent<GameObject> OnInteractEvent;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        interactable.selectEntered.AddListener(OnSelectEntered);
    }

    private void OnDestroy()
    {
        interactable.selectEntered.RemoveListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        GameObject interactorObject = args.interactorObject.transform.gameObject;

        if (interactorObject.CompareTag(targetTag))
        {
            OnInteractEvent.Invoke(interactorObject);
        }
    }
}
