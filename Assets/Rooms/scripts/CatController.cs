using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class CatController : MonoBehaviour
{
    private Animator anim;
    private bool shouldMove = false;
    public float moveDistance = 2f; // 新增移动距离
    public float walkSpeed = 0.01f;
    private Vector3 targetPosition; // 新增目标位置

    void Start()
    {
        anim = GetComponent<Animator>();
        targetPosition = transform.position + transform.forward * moveDistance; // 计算目标位置
    }

    // 新增移动触发方法
    public void StartMove()
    {
        StartCoroutine(WaitAndMove());
    }

    IEnumerator WaitAndMove()
    {
        yield return new WaitForSeconds(1f); // 等待1秒
        
        shouldMove = true;
        if(anim != null)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            Debug.LogError("Animator组件缺失");
        }
    }

    void Update()
    {
        if (shouldMove)
        {
            // 沿Z轴移动
            transform.position = Vector3.MoveTowards(
                transform.position, 
                targetPosition, 
                walkSpeed * Time.deltaTime
            );

            // 到达目标位置后停止
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                shouldMove = false;
                anim.SetBool("isMoving", false);
            }
        }

        // 移除原有的键盘输入检测代码
    }
}