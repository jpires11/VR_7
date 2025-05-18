using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VentKeyTrigger : MonoBehaviour
{
    //public string targetTag;
    public GameObject keyCat;
    public GameObject keyPlayer;
    public UnityEvent OnTrigger;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            if (keyCat != null)
            {
                keyCat.SetActive(false);
            }

            if (keyPlayer != null)
            {
                keyPlayer.SetActive(true);
            }

            //Debug.Log("Cat detected");
            OnTrigger.Invoke();
            hasTriggered = true;
        }
    }
}

