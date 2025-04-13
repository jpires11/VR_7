using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLineTrigger : MonoBehaviour
{
    public string targetTag;

    private void OnTriggerEnter(Collider other)
    {
        // You can filter here by tag if you only want specific objects to trigger it
        if (other.gameObject.tag == targetTag) // change as needed
        {
            AudioManager.instance.Play("Cat");
        }
    }
}
