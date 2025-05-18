using UnityEngine;
using UnityEngine.XR;

public class KittyFollowPlayer : MonoBehaviour
{
    // Player transform can be automatically obtained without manual assignment
    private Transform playerTransform;
    public float followSpeed = 0.0001f; // Reduced default speed
    public float stopDistance = 1f;
    private bool shouldFollow = false;
    
    // Animation parameter name
    private readonly string walkAnimParam = "IsWalking";
    // Direct reference to Animator component
    public Animator kittyAnimator;
    
    // Audio related variables
    private AudioSource catMeowAudio;
    private float soundTimer = 0f;
    public float soundInterval = 10f; // Audio play interval, default 10 seconds

    private void Start()
    {
        // Try to get main camera as player position reference
        playerTransform = Camera.main.transform;
        
        // If main camera not found, try to find XR origin
        if (playerTransform == null)
        {
            var xrOrigin = FindObjectOfType<Unity.XR.CoreUtils.XROrigin>();
            if (xrOrigin != null)
            {
                playerTransform = xrOrigin.transform;
            }
        }
        
        // Get cat meow audio source
        catMeowAudio = transform.Find("cat_meow")?.GetComponent<AudioSource>();
        if (catMeowAudio == null)
        {
            catMeowAudio = GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (shouldFollow && playerTransform != null)
        {
            // Update audio timer
            soundTimer += Time.deltaTime;
            
            // Play meow sound at specified intervals
            if (soundTimer >= soundInterval && catMeowAudio != null)
            {
                catMeowAudio.Play();
                soundTimer = 0f; // Reset timer
            }
            
            // Calculate distance to player
            float distance = Vector3.Distance(transform.position, playerTransform.position);

            // If distance is greater than stop distance, move and play walking animation
            if (distance > stopDistance)
            {
                // Set animation state to walking
                if (kittyAnimator != null)
                {
                    kittyAnimator.SetBool(walkAnimParam, true);
                }
                
                Vector3 direction = (playerTransform.position - transform.position).normalized;
                // Only move in horizontal direction (optional)
                direction.y = 0;
                direction = direction.normalized;
                
                transform.position += direction * followSpeed * Time.deltaTime;
                
                // Make Kitty face the player
                transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));
            }
            else
            {
                // Stop walking animation
                if (kittyAnimator != null)
                {
                    kittyAnimator.SetBool(walkAnimParam, false);
                }
            }
        }
        else
        {
            // Ensure walking animation is not playing when not following
            if (kittyAnimator != null)
            {
                kittyAnimator.SetBool(walkAnimParam, false);
            }
        }
    }

    public void StartFollowing()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Cannot find player position reference!");
            return;
        }
        shouldFollow = true;
        soundTimer = soundInterval; // Set timer to make kitty meow immediately when starting to follow
        Debug.Log("Kitty starts following player!");
    }
}