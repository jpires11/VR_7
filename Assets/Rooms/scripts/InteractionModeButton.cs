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
        Debug.Log("按钮被按下！");
        
        if (interactionManager != null)
        {
            Debug.Log("调用 ToggleInteractionMode");
            interactionManager.ToggleInteractionMode();
        }
        else
        {
            Debug.LogError("未找到交互模式管理器！");
        }
    }
}