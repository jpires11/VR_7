using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class CustomSocketInteraction1 : MonoBehaviour
{
    public string validTag = "Letter_C";
    public bool isSocketed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(validTag))
        {
            Debug.Log("Valid object detected!");
            isSocketed = true;
        }
        else
        {
            other.gameObject.SetActive(false);
            Debug.Log("Invalid object detected and disabled!");
            StartCoroutine(ReactivateObject(other.gameObject, 1f));
        }
    }

    private IEnumerator ReactivateObject(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(true);
        obj.transform.position += new Vector3(0, -0.4f, 0);
    }
}