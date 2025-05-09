using UnityEngine;

public class TruckTriggerZone : MonoBehaviour
{
    [Tooltip("The key object to activate when the truck enters.")]
    public GameObject keyObject;
    public GameObject keyBisObject;
    public GameObject truckObject;
    public GameObject truckBisObject;

    [Tooltip("Should the trigger work only once?")]
    public bool triggerOnce = true;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered && triggerOnce)
            return;

        // Check if the entering object is the truck
        if (other.CompareTag("Truck"))
        {
            //Debug.Log("Truck detected");
            // Show the key object
            if (keyObject != null)
            {
                keyObject.SetActive(false);
                //Debug.Log("Key object activated.");
            }

            if (keyBisObject != null)
            {
                keyBisObject.SetActive(true);
            }

            if (truckObject != null)
            {
                truckObject.SetActive(false);
            }

            if (truckBisObject != null)
            {
                truckBisObject.SetActive(true);
            }
            hasTriggered = true;
        }
    }
}
