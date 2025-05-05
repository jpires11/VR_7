using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class Plate : MonoBehaviour
{
    private Renderer objectRenderer;
    private Color originalColor;
    private bool hasBeenTouched = false;
    
    // 静态变量用于跟踪已触碰的盘子数量和总盘子数量
    private static int touchedPlatesCount = 0;
    private static int totalPlates = 3;
    
    // 引用LevelCompletion脚本
    private LevelCompletion levelCompletion;
    
    // 添加公共变量用于在Inspector中拖拽赋值
    public GameObject levelManager;
    
    // 在开始时获取渲染器组件并保存原始颜色
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
        else
        {
            Debug.LogError("没有找到Renderer组件，请确保该脚本附加到有Renderer组件的物体上");
        }
        
        if (levelManager != null)
        {
            levelCompletion = levelManager.GetComponent<LevelCompletion>();
            if (levelCompletion == null)
            {
                Debug.LogWarning("LevelManager上没有找到LevelCompletion脚本");
            }
        }
        else
        {
            Debug.LogWarning("场景中没有找到LevelManager对象");
        }
    }
    
    // 当发生碰撞时调用
    private void OnCollisionEnter(Collision collision)
    {
        if (!hasBeenTouched)
        {
            ChangeColorToYellow();
        }
    }
    
    // 当触发器碰撞时调用（如果使用的是触发器而非碰撞体）
    private void OnTriggerEnter(Collider other)
    {
        if (!hasBeenTouched)
        {
            ChangeColorToYellow();
        }
    }
    
    // 改变颜色为黄色的方法
    private void ChangeColorToYellow()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = Color.yellow;
            
            // 标记为已触碰并增加计数
            hasBeenTouched = true;
            touchedPlatesCount++;
            
            // Debug.Log("盘子被触碰，当前已触碰: " + touchedPlatesCount + "/" + totalPlates);
            
            // 检查是否所有盘子都被触碰
            CheckAllPlatesCompleted();
        }
    }
    
    // 检查是否所有盘子都被触碰
    private void CheckAllPlatesCompleted()
    {
        if (touchedPlatesCount >= totalPlates)
        {
            // 触发关卡完成事件
            if (levelCompletion != null)
            {
                // Debug.Log("所有盘子都已触碰！触发完成事件");
                levelCompletion.OnLevelCompleted();
            }
        }
    }
    
    // 可选：添加一个恢复原始颜色的方法
    public void ResetColor()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = originalColor;
            
            // 如果之前已触碰，则减少计数
            if (hasBeenTouched)
            {
                hasBeenTouched = false;
                touchedPlatesCount--;
            }
        }
    }
    
    // 当场景重新加载或游戏重启时重置静态计数器
    private void OnDestroy()
    {
        // 重置静态变量，防止在场景重新加载时计数器保持旧值
        touchedPlatesCount = 0;
    }
}