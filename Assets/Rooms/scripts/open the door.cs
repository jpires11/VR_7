using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; // Import XR Interaction Toolkit

public class OpenTheDoor : MonoBehaviour
{
    [Header("门动画设置")]
    [SerializeField] private Animator doorAnimator; // 门的动画控制器
    [SerializeField] private string openAnimationTrigger = "Open"; // 开门动画的触发器名称
    
    [Header("交互设置")]
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socketInteractor; // Socket交互器

    [Header("事件触发")]
    [SerializeField] private UnityEngine.Events.UnityEvent onDoorOpened; // 新增事件触发器
    
    
    private bool doorOpened = false; // 门的状态标记

    private void Awake()
    {
        // 如果没有指定动画控制器，尝试从当前游戏对象获取
        if (doorAnimator == null)
        {
            doorAnimator = GetComponent<Animator>();
        }
        
        // 如果没有指定Socket交互器，尝试从当前游戏对象获取
        if (socketInteractor == null)
        {
            socketInteractor = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
        }
    }

    private void OnEnable()
    {
        // 注册Socket交互事件
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.AddListener(OnObjectPlaced);
        }
        else
        {
            Debug.LogError("Socket交互器未设置，请在Inspector中指定XRSocketInteractor组件");
        }
    }

    private void OnDisable()
    {
        // 取消注册事件
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.RemoveListener(OnObjectPlaced);
        }
    }

    // 当物体放入Socket时触发
    private void OnObjectPlaced(SelectEnterEventArgs args)
    {
        if (!doorOpened)
        {
            OpenDoor();
        }
    }

    // 打开门
    private void OpenDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger(openAnimationTrigger);
            doorOpened = true;
            Debug.Log("门已打开");
            
            // 新增事件触发
            onDoorOpened?.Invoke();
        }
        else
        {
            Debug.LogError("未找到Animator组件，无法播放开门动画");
        }
    }
}