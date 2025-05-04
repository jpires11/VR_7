using UnityEngine;

public class LevelCompletion : MonoBehaviour
{
    // 引用到显示关卡完成信息的 Canvas 或者UI对象
    public GameObject levelCompleteUI;  
    
    // 在开始时隐藏UI
    void Start()
    {
        // 确保UI一开始是隐藏的
        if(levelCompleteUI != null)
            levelCompleteUI.SetActive(false);
    }

    // 模拟关卡完成的条件，比如外部调用此方法
    public void OnLevelCompleted()
    {
        Debug.Log("准备激活UI元素");
        
        // 直接激活UI对象，不再重新定位
        if(levelCompleteUI != null)
            levelCompleteUI.SetActive(true);
        Debug.Log("UI元素已激活");
    }
}