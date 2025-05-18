using UnityEngine;

public class CatVisionReveal : MonoBehaviour
{
    public GameObject hiddenText; 

    private void Start()
    {
        if (hiddenText != null)
        {
            hiddenText.SetActive(false); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cat")) 
        {
            if (hiddenText != null)
            {
                hiddenText.SetActive(true); 
                // Debug.Log("Text Active");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cat"))
        {
            if (hiddenText != null)
            {
                hiddenText.SetActive(false); 
                // Debug.Log("Text Inactive");
            }
        }
    }
}
