using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class collide_hide : MonoBehaviour
{
    // Optional: Set delay time before hiding after collision
    [SerializeField] private float hideDelay = 0.0f;
    
    // Optional: Specify which tags can trigger hiding
    [SerializeField] private string[] triggerTags;
    
    private MeshRenderer meshRenderer;
    private Collider objectCollider;
    
    private void Start()
    {
        // Get the MeshRenderer component
        meshRenderer = GetComponent<MeshRenderer>();
        
        // Get the collider component
        objectCollider = GetComponent<Collider>();
        
        // Ensure the object has a collider
        if (objectCollider == null)
        {
            Debug.LogWarning("Object has no collider component, collision detection will not work!");
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        // Check if collision object has Box Collider
        if (collision.collider is BoxCollider)
        {
            // If trigger tags are set, check collision object's tag
            if (triggerTags != null && triggerTags.Length > 0)
            {
                bool tagMatch = false;
                foreach (string tag in triggerTags)
                {
                    if (collision.gameObject.CompareTag(tag))
                    {
                        tagMatch = true;
                        break;
                    }
                }
                
                if (!tagMatch)
                    return; // If no matching tag, don't hide
            }
            
            // If there's a delay, use coroutine
            if (hideDelay > 0)
            {
                StartCoroutine(HideAfterDelay());
            }
            else
            {
                // Hide object immediately
                HideObject();
            }
        }
    }
    
    // Method for trigger collision
    private void OnTriggerEnter(Collider other)
    {
        // Check if collision object has Box Collider
        if (other is BoxCollider)
        {
            // If trigger tags are set, check collision object's tag
            if (triggerTags != null && triggerTags.Length > 0)
            {
                bool tagMatch = false;
                foreach (string tag in triggerTags)
                {
                    if (other.gameObject.CompareTag(tag))
                    {
                        tagMatch = true;
                        break;
                    }
                }
                
                if (!tagMatch)
                    return; // If no matching tag, don't hide
            }
            
            // If there's a delay, use coroutine
            if (hideDelay > 0)
            {
                StartCoroutine(HideAfterDelay());
            }
            else
            {
                // Hide object immediately
                HideObject();
            }
        }
    }
    
    // Coroutine for delayed hiding
    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(hideDelay);
        HideObject();
    }
    
    // Method to hide object
    private void HideObject()
    {
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }
        
        // Optional: Can also disable the collider to prevent further collisions
        // if (objectCollider != null)
        // {
        //     objectCollider.enabled = false;
        // }
    }
}