using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomSocketInteraction2 : MonoBehaviour
{
    public string validTag = "Letter_A";

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