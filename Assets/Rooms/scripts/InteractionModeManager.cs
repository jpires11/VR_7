using UnityEngine;


public class InteractionModeManager : MonoBehaviour
{
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor leftRayInteractor;
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor rightRayInteractor;
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRDirectInteractor leftDirectInteractor;
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRDirectInteractor rightDirectInteractor;
    
    private bool laserInteractionEnabled = false;
    
    private void Start()
    {
        // 默认设置：启用近距离交互，禁用激光交互
        SetNearFarInteractionOnly();
    }
    
    public void ToggleInteractionMode()
    {
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
        // 启用近距离交互器
        if (leftDirectInteractor != null) leftDirectInteractor.enabled = true;
        if (rightDirectInteractor != null) rightDirectInteractor.enabled = true;
        
        // 禁用激光交互器
        if (leftRayInteractor != null) leftRayInteractor.enabled = false;
        if (rightRayInteractor != null) rightRayInteractor.enabled = false;
        
        Debug.Log("已切换到近距离交互模式");
    }
    
    private void EnableLaserInteraction()
    {
        // 保持近距离交互器启用
        if (leftDirectInteractor != null) leftDirectInteractor.enabled = true;
        if (rightDirectInteractor != null) rightDirectInteractor.enabled = true;
        
        // 同时启用激光交互器
        if (leftRayInteractor != null) leftRayInteractor.enabled = true;
        if (rightRayInteractor != null) rightRayInteractor.enabled = true;
        
        Debug.Log("已启用激光交互模式");
    }
}