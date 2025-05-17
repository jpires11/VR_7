using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractionModeButton : MonoBehaviour
{
    [SerializeField] private InteractionModeManager interactionManager;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable;
    
    private void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        
        if (interactable == null)
        {
            interactable = gameObject.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        }
        
        if (interactionManager == null)
        {
            interactionManager = FindObjectOfType<InteractionModeManager>();
        }
    }
    
    private void OnEnable()
    {
        interactable.selectEntered.AddListener(OnButtonPressed);
    }
    
    private void OnDisable()
    {
        interactable.selectEntered.RemoveListener(OnButtonPressed);
    }
    
    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        if (interactionManager != null)
        {
            interactionManager.ToggleInteractionMode();
        }
        else
        {
            Debug.LogError("未找到交互模式管理器！");
        }
    }
}