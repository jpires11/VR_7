using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class collide_hide : MonoBehaviour
{
    // 可选：设置碰撞后隐藏的延迟时间
    [SerializeField] private float hideDelay = 0.0f;
    
    // 可选：指定哪些标签的物体可以触发隐藏
    [SerializeField] private string[] triggerTags;
    
    private MeshRenderer meshRenderer;
    private Collider objectCollider;
    
    private void Start()
    {
        // 获取物体的MeshRenderer组件
        meshRenderer = GetComponent<MeshRenderer>();
        
        // 获取物体的碰撞器组件
        objectCollider = GetComponent<Collider>();
        
        // 确保物体有碰撞器
        if (objectCollider == null)
        {
            Debug.LogWarning("物体没有碰撞器组件，碰撞检测将无法工作！");
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        // 检查碰撞物体是否有Box Collider
        if (collision.collider is BoxCollider)
        {
            // 如果设置了触发标签，则检查碰撞物体的标签
            if (triggerTags != null && triggerTags.Length > 0)
            {
                bool tagMatch = false;
                foreach (string tag in triggerTags)
                {
                    if (collision.gameObject.CompareTag(tag))
                    {
                        tagMatch = true;
                        break;
                    }
                }
                
                if (!tagMatch)
                    return; // 如果没有匹配的标签，则不隐藏
            }
            
            // 如果有延迟，则使用协程
            if (hideDelay > 0)
            {
                StartCoroutine(HideAfterDelay());
            }
            else
            {
                // 立即隐藏物体
                HideObject();
            }
        }
    }
    
    // 用于触发器碰撞的方法
    private void OnTriggerEnter(Collider other)
    {
        // 检查碰撞物体是否有Box Collider
        if (other is BoxCollider)
        {
            // 如果设置了触发标签，则检查碰撞物体的标签
            if (triggerTags != null && triggerTags.Length > 0)
            {
                bool tagMatch = false;
                foreach (string tag in triggerTags)
                {
                    if (other.gameObject.CompareTag(tag))
                    {
                        tagMatch = true;
                        break;
                    }
                }
                
                if (!tagMatch)
                    return; // 如果没有匹配的标签，则不隐藏
            }
            
            // 如果有延迟，则使用协程
            if (hideDelay > 0)
            {
                StartCoroutine(HideAfterDelay());
            }
            else
            {
                // 立即隐藏物体
                HideObject();
            }
        }
    }
    
    // 延迟隐藏的协程
    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(hideDelay);
        HideObject();
    }
    
    // 隐藏物体的方法
    private void HideObject()
    {
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }
        
        // 可选：也可以禁用碰撞器，防止进一步的碰撞
        // if (objectCollider != null)
        // {
        //     objectCollider.enabled = false;
        // }
    }
}