using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class Plate : MonoBehaviour
{
    private Renderer objectRenderer;
    private Color originalColor;
    
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
    }
    
    // 当发生碰撞时调用
    private void OnCollisionEnter(Collision collision)
    {
        ChangeColorToYellow();
    }
    
    // 当触发器碰撞时调用（如果使用的是触发器而非碰撞体）
    private void OnTriggerEnter(Collider other)
    {
        ChangeColorToYellow();
    }
    
    // 改变颜色为黄色的方法
    private void ChangeColorToYellow()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = Color.yellow;
        }
    }
    
    // 可选：添加一个恢复原始颜色的方法
    public void ResetColor()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = originalColor;
        }
    }
}