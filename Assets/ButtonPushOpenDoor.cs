using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonPushOpenDoor : MonoBehaviour
{
    public Animator animator;
    public string boolName = "Open";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>().selectEntered.AddListener(x => ToggleDoorOpen());
    }

    public void ToggleDoorOpen()
    {
        bool isOpen = animator.GetBool(boolName);
        animator.SetBool(boolName, !isOpen);

        AudioManager.instance.Play("Door");
    }
}
