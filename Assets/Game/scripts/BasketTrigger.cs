using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketTrigger : MonoBehaviour
{
    //public string targetTag;
    public GameObject reward;
    
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered)
        {
            return;
        }

        if (other.CompareTag("Ball"))
        {
            if (reward != null)
            {
                reward.SetActive(true);
            }            
            
            hasTriggered = true;
        }
    }
}

