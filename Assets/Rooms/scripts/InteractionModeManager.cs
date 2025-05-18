﻿using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractionModeManager : MonoBehaviour
{
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor leftRayInteractor;
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor rightRayInteractor;
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRDirectInteractor leftDirectInteractor;
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRDirectInteractor rightDirectInteractor;
    [SerializeField] private KittyFollowLaser kittyFollowLaser; 
    
    private bool laserInteractionEnabled = false;
    private bool isEnabled = false; // Flag indicating if the system is enabled
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable; 
    
    private void Awake()
    {
        // Get or add interaction component
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        
        if (interactable == null)
        {
            interactable = gameObject.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        }
        
        // If kitty follow laser script is not specified, try to find it in the scene
        if (kittyFollowLaser == null)
        {
            kittyFollowLaser = FindObjectOfType<KittyFollowLaser>();
        }
    }
    
    private void Start()
    {
        // Default setting: disable all interactors until button is pressed
        DisableAllInteractors();
    }
    
    private void OnEnable()
    {
        // Register button events
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnButtonPressed);
        }
    }
    
    private void OnDisable()
    {
        // Remove button events
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnButtonPressed);
        }
    }
    
    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        Debug.Log("Button pressed!");
        
        // Toggle interaction mode
        ToggleInteractionMode();
        
        // Enable kitty follow laser functionality
        if (kittyFollowLaser != null)
        {
            Debug.Log("Enable kitty follow laser functionality");
            kittyFollowLaser.StartFollowing();
        }
        else
        {
            Debug.LogWarning("Kitty follow laser script not found!");
        }
    }

    public void OnUnityButtonClick()
    {
        Debug.Log("Unity button clicked!");
        
        // Toggle interaction mode
        ToggleInteractionMode();
        
        // Enable kitty follow laser functionality
        if (kittyFollowLaser != null)
        {
            Debug.Log("Enable kitty follow laser functionality");
            kittyFollowLaser.StartFollowing();
        }
        else
        {
            Debug.LogWarning("Kitty follow laser script not found!");
        }
    }
    
    // Method to disable all interactors
    private void DisableAllInteractors()
    {
        if (leftDirectInteractor != null) leftDirectInteractor.enabled = false;
        if (rightDirectInteractor != null) rightDirectInteractor.enabled = false;
        if (leftRayInteractor != null) leftRayInteractor.enabled = false;
        if (rightRayInteractor != null) rightRayInteractor.enabled = false;
        
        Debug.Log("All interactors disabled, waiting for button activation");
    }
    
    // Method to enable the system
    public void EnableSystem()
    {
        if (!isEnabled)
        {
            isEnabled = true;
            // Default set to near interaction mode
            SetNearFarInteractionOnly();
            Debug.Log("Interaction system enabled");
        }
    }
    
    public void ToggleInteractionMode()
    {
        // Can only switch modes after system is enabled
        if (!isEnabled)
        {
            EnableSystem();
            return;
        }
        
        laserInteractionEnabled = !laserInteractionEnabled;
        
        if (laserInteractionEnabled)
        {
            EnableLaserInteraction();
        }
        else
        {
            SetNearFarInteractionOnly();
        }
    }
    
    private void SetNearFarInteractionOnly()
    {
        // Enable near interaction interactors
        if (leftDirectInteractor != null) leftDirectInteractor.enabled = true;
        if (rightDirectInteractor != null) rightDirectInteractor.enabled = true;
        
        // Disable laser interactors
        if (leftRayInteractor != null) leftRayInteractor.enabled = false;
        if (rightRayInteractor != null) rightRayInteractor.enabled = false;
        
        Debug.Log("Switched to near interaction mode");
    }
    
    private void EnableLaserInteraction()
    {
        // Keep near interaction interactors enabled
        if (leftDirectInteractor != null) leftDirectInteractor.enabled = true;
        if (rightDirectInteractor != null) rightDirectInteractor.enabled = true;
        
        // Also enable laser interactors
        if (leftRayInteractor != null) leftRayInteractor.enabled = true;
        if (rightRayInteractor != null) rightRayInteractor.enabled = true;
        
        Debug.Log("Laser interaction mode enabled");
    }
}