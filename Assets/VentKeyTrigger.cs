using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentKeyTrigger : MonoBehaviour
{
    //public string targetTag;
    public GameObject keyCat;
    public GameObject keyPlayer;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered)
        {
            return;
        }

        if (other.CompareTag("Key"))
        {
            if (keyCat != null)
            {
                keyCat.SetActive(false);
            }

            if (keyPlayer != null)
            {
                keyPlayer.SetActive(true);
            }           
            
            hasTriggered = true;
        }
    }
}

