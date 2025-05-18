﻿using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractionModeManager : MonoBehaviour
{
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor leftRayInteractor;
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor rightRayInteractor;
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRDirectInteractor leftDirectInteractor;
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRDirectInteractor rightDirectInteractor;
    [SerializeField] private KittyFollowLaser kittyFollowLaser; // 新增：引用猫咪跟随激光脚本
    
    private bool laserInteractionEnabled = false;
    private bool isEnabled = false; // 系统是否启用的标志
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable; // 新增：按钮交互组件
    
    private void Awake()
    {
        // 获取或添加交互组件
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        
        if (interactable == null)
        {
            interactable = gameObject.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        }
        
        // 如果没有指定猫咪跟随激光脚本，尝试在场景中查找
        if (kittyFollowLaser == null)
        {
            kittyFollowLaser = FindObjectOfType<KittyFollowLaser>();
        }
    }
    
    private void Start()
    {
        // 默认设置：禁用所有交互器，直到按钮被按下
        DisableAllInteractors();
    }
    
    private void OnEnable()
    {
        // 注册按钮事件
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnButtonPressed);
        }
    }
    
    private void OnDisable()
    {
        // 移除按钮事件
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnButtonPressed);
        }
    }
    
    // 新增：按钮按下事件处理
    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        Debug.Log("按钮被按下！");
        
        // 切换交互模式
        ToggleInteractionMode();
        
        // 启用猫咪跟随激光功能
        if (kittyFollowLaser != null)
        {
            Debug.Log("启用猫咪跟随激光功能");
            kittyFollowLaser.StartFollowing();
        }
        else
        {
            Debug.LogWarning("未找到猫咪跟随激光脚本！");
        }
    }

    public void OnUnityButtonClick()
    {
        Debug.Log("Unity按钮被点击！");
        
        // 切换交互模式
        ToggleInteractionMode();
        
        // 启用猫咪跟随激光功能
        if (kittyFollowLaser != null)
        {
            Debug.Log("启用猫咪跟随激光功能");
            kittyFollowLaser.StartFollowing();
        }
        else
        {
            Debug.LogWarning("未找到猫咪跟随激光脚本！");
        }
    }
    
    // 禁用所有交互器的方法
    private void DisableAllInteractors()
    {
        if (leftDirectInteractor != null) leftDirectInteractor.enabled = false;
        if (rightDirectInteractor != null) rightDirectInteractor.enabled = false;
        if (leftRayInteractor != null) leftRayInteractor.enabled = false;
        if (rightRayInteractor != null) rightRayInteractor.enabled = false;
        
        Debug.Log("所有交互器已禁用，等待按钮激活");
    }
    
    // 启用系统的方法
    public void EnableSystem()
    {
        if (!isEnabled)
        {
            isEnabled = true;
            // 默认设置为近距离交互模式
            SetNearFarInteractionOnly();
            Debug.Log("交互系统已启用");
        }
    }
    
    public void ToggleInteractionMode()
    {
        // 只有在系统启用后才能切换模式
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