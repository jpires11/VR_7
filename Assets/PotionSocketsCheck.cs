using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PotionSocketsCheck : MonoBehaviour
{
    public List<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor> sockets;
    private bool triggered = false;
    public List<GameObject> plateforms;

    void OnEnable()
    {
        foreach (var socket in sockets)
        {
            socket.selectEntered.AddListener(OnItemPlaced);
        }
    }

    void OnDisable()
    {
        foreach (var socket in sockets)
        {
            socket.selectEntered.RemoveListener(OnItemPlaced);
        }
    }

    void OnItemPlaced(SelectEnterEventArgs args)
    {
        if (triggered) return;

        // Check if all sockets have at least one interactable selected
        foreach (var socket in sockets)
        {
            if (!socket.hasSelection)
                return; // One or more sockets are still empty
        }

        triggered = true;
        OnAllSocketsFilled();
    }

    private void OnAllSocketsFilled()
    {
        foreach (var piece in plateforms)
        {
            piece.SetActive(true);
        }
    }
}
