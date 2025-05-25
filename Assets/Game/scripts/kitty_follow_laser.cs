using UnityEngine;
using UnityEngine.XR;

public class KittyFollowLaser : MonoBehaviour
{
    // Laser pointer related
    public Transform laserPointer; // Transform of the laser pointer
    private Vector3 targetPosition; // Target position pointed by laser
    
    public float followSpeed = 1.0f; // Significantly increase default speed
    public float stopDistance = 0.5f; // Stop distance when reaching target point
    private bool shouldFollow = true; 
    
    // Animation parameter name
    private readonly string walkAnimParam = "IsWalking";
    // Direct reference to Animator component
    public Animator kittyAnimator;
    
    // Add audio related variables
    private AudioSource catMeowAudio;
    private float soundTimer = 0f;
    public float soundInterval = 10f; // Audio play interval, default 10 seconds
    
    // Add debug variables
    private float debugTimer = 0f;
    public float debugInterval = 1f; // Print position info every second

    private void Start()
    {
        // Try to get cat meow audio source
        catMeowAudio = transform.Find("cat_meow")?.GetComponent<AudioSource>();
        if (catMeowAudio == null)
        {
            catMeowAudio = GetComponent<AudioSource>();
        }
        
        // Note: No longer using tag search, need to manually specify laserPointer in Inspector
        if (laserPointer == null)
        {
            Debug.LogWarning("Laser pointer not specified, please set laserPointer reference in Inspector!");
        }
    }

    private void Update()
    {
        // Debug output laser position info
        debugTimer += Time.deltaTime;
        if (debugTimer >= debugInterval && laserPointer != null)
        {
            UpdateLaserTargetPosition(); // Update target position
            // Debug.Log($"Laser pointing position: {targetPosition}, Kitty position: {transform.position}, Distance: {Vector3.Distance(transform.position, targetPosition)}, Following: {shouldFollow}");
            debugTimer = 0f;
        }
        
        if (shouldFollow && laserPointer != null)
        {
            // Update target position - use raycast to get laser pointing position
            UpdateLaserTargetPosition();
            
            // Update audio timer
            soundTimer += Time.deltaTime;
            
            // Play cat meow sound at specified intervals
            if (soundTimer >= soundInterval && catMeowAudio != null)
            {
                catMeowAudio.Play();
                soundTimer = 0f; // Reset timer
            }
            
            // Calculate distance to target position
            float distance = Vector3.Distance(transform.position, targetPosition);

            // If distance is greater than stop distance, move and play walking animation
            if (distance > stopDistance)
            {
                // Set animation state to walking
                if (kittyAnimator != null)
                {
                    kittyAnimator.SetBool(walkAnimParam, true);
                }
                
                Vector3 direction = (targetPosition - transform.position).normalized;
                // Only move in horizontal direction
                direction.y = 0;
                direction = direction.normalized;
                
                // Use more noticeable movement speed
                float moveStep = followSpeed * Time.deltaTime;
                transform.position += direction * moveStep;
                
                // Make Kitty face target position
                transform.LookAt(new Vector3(targetPosition.x, transform.position.y, targetPosition.z));
                
                // Add debug output
                // Debug.Log($"Kitty is moving! Direction: {direction}, Step: {moveStep}, New position: {transform.position}");
            }
            else
            {
                // Stop walking animation
                if (kittyAnimator != null)
                {
                    kittyAnimator.SetBool(walkAnimParam, false);
                }
                // Debug.Log("Kitty reached target position, stopping movement");
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
    
    // Update laser pointing target position
    private void UpdateLaserTargetPosition()
    {
        if (laserPointer != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(laserPointer.position, laserPointer.forward, out hit))
            {
                // Laser hit object, use hit point as target
                targetPosition = hit.point;
                // Debug.Log($"Laser ray hit: {hit.collider.name}, Position: {hit.point}");
            }
            else
            {
                // Laser didn't hit object, use a far point as target
                targetPosition = laserPointer.position + laserPointer.forward * 100f;
                // Debug.Log("Laser ray didn't hit any object, using far point");
            }
            
            // Ensure target position is on ground (optional)
            targetPosition.y = transform.position.y;
        }
    }

    public void StartFollowing()
    {
        if (laserPointer == null)
        {
            // Debug.LogError("Cannot find laser pointer!");
            return;
        }
        shouldFollow = true;
        soundTimer = soundInterval; // Set timer to make kitty meow immediately when starting to follow
        Debug.Log("Kitty starts following laser point!");
    }
}