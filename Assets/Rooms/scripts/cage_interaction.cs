using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class CageInteraction : MonoBehaviour
{
    public CustomSocketInteraction1 socket1;
    public CustomSocketInteraction2 socket2;
    public CustomSocketInteraction3 socket3; 
    
    private bool isDoorOpen = false;

    private void Update()
    {
        if (!isDoorOpen && socket1 != null && socket2 != null && socket3 != null && 
            socket1.isSocketed && socket2.isSocketed && socket3.isSocketed)
        {
            Debug.LogWarning("Yes! the cage will open!");
            OpenDoor();
            isDoorOpen = true;
        }
    }

    private void OpenDoor()
    {
        Debug.Log("Door is opening...");
        StartCoroutine(OpenDoorWithDelay());
    }

    private IEnumerator OpenDoorWithDelay()
    {
        yield return new WaitForSeconds(2); 
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("open_cage"); 
            Debug.LogWarning("trigger open_cage!");
        }
        else
        {
            Debug.LogWarning("Animator component not found!");
        }
    }
}