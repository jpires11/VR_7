using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomSocketInteraction : MonoBehaviour
{
    public string validTag = "Letter_C";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(validTag))
        {
            Debug.Log("Valid object detected!");
        }
        else
        {
            other.gameObject.SetActive(false);
            Debug.Log("Invalid object detected and disabled!");
        }
    }
}