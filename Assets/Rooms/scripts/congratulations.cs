using UnityEngine;

public class LevelCompletion : MonoBehaviour
{
    // 引用到显示关卡完成信息的 Canvas 或者UI对象
    public GameObject levelCompleteUI;  
    // 用于播放动画的 Animator 组件
    public Animator levelCompleteAnimator;
    
    // 设置动画的触发器名称, 和 Animator 控制器中对应的触发器名称一致
    public string animationTriggerName = "ShowComplete";

    // 模拟关卡完成的条件，比如外部调用此方法
    public void OnLevelCompleted()
    {
        // 将UI置于玩家视野前方
        PlaceUIInFrontOfPlayer();
        
        // 激活UI对象
        if(levelCompleteUI != null)
            levelCompleteUI.SetActive(true);
        
        // 触发动画
        if(levelCompleteAnimator != null)
            levelCompleteAnimator.SetTrigger(animationTriggerName);
    }

    // 将UI放置在玩家（摄像机）前方一定距离处
    void PlaceUIInFrontOfPlayer()
    {
        Camera cam = Camera.main;
        if (cam != null && levelCompleteUI != null)
        {
            // 设置距离（例如 2 米）
            float distance = 2f;
            // 计算正前方的位置
            Vector3 forwardPosition = cam.transform.position + cam.transform.forward * distance;
            // 确保UI朝向玩家
            levelCompleteUI.transform.position = forwardPosition;
            levelCompleteUI.transform.LookAt(cam.transform);
            // 为了防止UI反向，可以加180度旋转
            levelCompleteUI.transform.Rotate(0, 180, 0);
        }
    }
}
