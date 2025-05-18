using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class Plate : MonoBehaviour
{
    private Renderer objectRenderer;
    private Color originalColor;
    private bool hasBeenTouched = false;
    
    // Static variables to track the number of touched plates and total plates
    private static int touchedPlatesCount = 0;
    private static int totalPlates = 3;
    
    // Reference to LevelCompletion script
    private LevelCompletion levelCompletion;
    
    // Public variables for drag and drop assignment in Inspector
    public GameObject levelManager;
    public GameObject fishbone;
    
    // Variables for random movement
    public float moveSpeed = 0.2f;         // Movement speed
    public float maxMoveDistance = 0.5f;   // Maximum movement distance
    private Vector3 originalPosition;       // Original position
    private Vector3 targetPosition;         // Target position
    private float moveTimer;                // Movement timer
    private float moveInterval = 3.0f;      // Interval for changing movement target
    
    // Get renderer component and save original color at start
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
        else
        {
            Debug.LogError("Renderer component not found, please ensure this script is attached to an object with a Renderer component");
        }
        
        if (levelManager != null)
        {
            levelCompletion = levelManager.GetComponent<LevelCompletion>();
            if (levelCompletion == null)
            {
                Debug.LogWarning("LevelCompletion script not found on LevelManager");
            }
        }
        else
        {
            Debug.LogWarning("LevelManager object not found in scene");
        }
        if (fishbone != null)
        {
            fishbone.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Fishbone object not found, please set it in Inspector");
        }
        
        // Initialize random movement
        originalPosition = transform.position;
        SetNewRandomTarget();
    }
    
    // Add Update method for handling random movement
    void Update()
    {
        // Handle random movement
        moveTimer -= Time.deltaTime;
        if (moveTimer <= 0)
        {
            SetNewRandomTarget();
        }
        
        // Smoothly move to target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
    
    // Set new random target position
    private void SetNewRandomTarget()
    {
        // Generate a random point around the original position
        Vector3 randomOffset = new Vector3(
            Random.Range(-maxMoveDistance, maxMoveDistance),
            Random.Range(-maxMoveDistance, maxMoveDistance),
            Random.Range(-maxMoveDistance, maxMoveDistance)
        );
        
        targetPosition = originalPosition + randomOffset;
        moveTimer = moveInterval;
    }
    
    // Called when collision occurs
    private void OnCollisionEnter(Collision collision)
    {
        if (!hasBeenTouched)
        {
            ChangeColorToYellow();
        }
    }
    
    // Called when trigger collision occurs (if using triggers instead of colliders)
    private void OnTriggerEnter(Collider other)
    {
        if (!hasBeenTouched)
        {
            ChangeColorToYellow();
        }
    }
    
    // Method to change color to yellow
    private void ChangeColorToYellow()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = Color.yellow;
            
            // Mark as touched and increment counter
            hasBeenTouched = true;
            touchedPlatesCount++;
            
            // Debug.Log("Plate touched, current count: " + touchedPlatesCount + "/" + totalPlates);
            
            // Hide fishbone when plate is touched
            if (fishbone != null)
            {
                fishbone.SetActive(false);
            }
            
            // Check if all plates have been touched
            CheckAllPlatesCompleted();
        }
    }
    
    // Check if all plates have been touched
    private void CheckAllPlatesCompleted()
    {
        if (touchedPlatesCount >= totalPlates)
        {
            // Trigger level completion event
            if (levelCompletion != null)
            {
                // Debug.Log("All plates have been touched! Triggering completion event");
                levelCompletion.OnLevelCompleted();
            }
        }
    }
    
    // Optional: Method to restore original color
    public void ResetColor()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = originalColor;
            
            // If previously touched, decrement counter
            if (hasBeenTouched)
            {
                hasBeenTouched = false;
                touchedPlatesCount--;
                
                // Make fishbone visible again on reset
                if (fishbone != null)
                {
                    fishbone.SetActive(true);
                }
            }
        }
    }
    
    // Reset static counter when scene reloads or game restarts
    private void OnDestroy()
    {
        // Reset static variable to prevent counter from keeping old value when scene reloads
        touchedPlatesCount = 0;
    }
}