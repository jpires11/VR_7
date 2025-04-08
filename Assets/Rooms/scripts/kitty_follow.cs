using UnityEngine;
using UnityEngine.XR;

public class KittyFollowPlayer : MonoBehaviour
{
    // 可以不需要手动指定player，自动获取
    private Transform playerTransform;
    public float followSpeed = 0.0001f; // 降低默认速度
    public float stopDistance = 1f;
    private bool shouldFollow = false;
    
    // 动画参数名称
    private readonly string walkAnimParam = "IsWalking";
    // 直接引用Animator组件
    public Animator kittyAnimator;
    
    // 添加音频相关变量
    private AudioSource catMeowAudio;
    private float soundTimer = 0f;
    public float soundInterval = 10f; // 音频播放间隔，默认10秒

    private void Start()
    {
        // 尝试获取主摄像机作为玩家位置参考
        playerTransform = Camera.main.transform;
        
        // 如果找不到主摄像机，尝试查找XR原点
        if (playerTransform == null)
        {
            var xrOrigin = FindObjectOfType<Unity.XR.CoreUtils.XROrigin>();
            if (xrOrigin != null)
            {
                playerTransform = xrOrigin.transform;
            }
        }
        
        // 获取猫叫声音频源
        catMeowAudio = transform.Find("cat_meow")?.GetComponent<AudioSource>();
        if (catMeowAudio == null)
        {
            catMeowAudio = GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (shouldFollow && playerTransform != null)
        {
            // 更新音频计时器
            soundTimer += Time.deltaTime;
            
            // 每隔指定时间播放一次猫叫声
            if (soundTimer >= soundInterval && catMeowAudio != null)
            {
                catMeowAudio.Play();
                soundTimer = 0f; // 重置计时器
            }
            
            // 计算与玩家的距离
            float distance = Vector3.Distance(transform.position, playerTransform.position);

            // 如果距离大于停止距离，则移动并播放行走动画
            if (distance > stopDistance)
            {
                // 设置动画状态为行走
                if (kittyAnimator != null)
                {
                    kittyAnimator.SetBool(walkAnimParam, true);
                }
                
                Vector3 direction = (playerTransform.position - transform.position).normalized;
                // 只在水平方向上移动（可选）
                direction.y = 0;
                direction = direction.normalized;
                
                transform.position += direction * followSpeed * Time.deltaTime;
                
                // 让Kitty朝向玩家
                transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));
            }
            else
            {
                // 停止行走动画
                if (kittyAnimator != null)
                {
                    kittyAnimator.SetBool(walkAnimParam, false);
                }
            }
        }
        else
        {
            // 确保不跟随时不播放行走动画
            if (kittyAnimator != null)
            {
                kittyAnimator.SetBool(walkAnimParam, false);
            }
        }
    }

    public void StartFollowing()
    {
        if (playerTransform == null)
        {
            Debug.LogError("无法找到玩家位置参考点！");
            return;
        }
        shouldFollow = true;
        soundTimer = soundInterval; // 设置计时器，使猫咪开始跟随时立即发出叫声
        Debug.Log("Kitty开始跟随玩家！");
    }
}