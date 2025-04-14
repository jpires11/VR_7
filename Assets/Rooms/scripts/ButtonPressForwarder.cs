using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonPressForwarder : MonoBehaviour
{
    private SimpleButtonTrigger manager;

    private void Start()
    {
        manager = FindObjectOfType<SimpleButtonTrigger>();

        var interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        interactable.selectEntered.AddListener(OnPress);
    }

    private void OnPress(SelectEnterEventArgs args)
    {
        manager.ButtonPressed(gameObject);
    }
}
