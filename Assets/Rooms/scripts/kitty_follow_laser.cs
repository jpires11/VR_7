using UnityEngine;
using UnityEngine.XR;

public class KittyFollowPlayer : MonoBehaviour
{
    // 可以不需要手动指定player，自动获取
    private Transform playerTransform;
    public float followSpeed = 0.0001f; // 降低默认速度
    public float stopDistance = 1f;
    private bool shouldFollow = false;
    
    // 添加激光指针相关变量
    public bool followLaser = false; // 是否跟随激光指针
    private Vector3 laserTargetPosition; // 激光指针指向的位置
    public Transform laserPointer; // 激光指针的Transform
    public LayerMask laserHitLayers; // 激光可以击中的层
    
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
        
        // 如果没有指定激光指针，尝试在玩家手柄上查找
        if (laserPointer == null && playerTransform != null)
        {
            // 尝试查找激光指针对象
            laserPointer = playerTransform.GetComponentInChildren<LineRenderer>()?.transform;
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
            
            // 确定目标位置
            Vector3 targetPosition;
            
            if (followLaser && laserPointer != null)
            {
                // 更新激光指向的位置
                UpdateLaserTargetPosition();
                targetPosition = laserTargetPosition;
            }
            else
            {
                // 使用玩家位置作为目标
                targetPosition = playerTransform.position;
            }
            
            // 计算与目标的距离
            float distance = Vector3.Distance(transform.position, targetPosition);

            // 如果距离大于停止距离，则移动并播放行走动画
            if (distance > stopDistance)
            {
                // 设置动画状态为行走
                if (kittyAnimator != null)
                {
                    kittyAnimator.SetBool(walkAnimParam, true);
                }
                
                Vector3 direction = (targetPosition - transform.position).normalized;
                // 只在水平方向上移动（可选）
                direction.y = 0;
                direction = direction.normalized;
                
                transform.position += direction * followSpeed * Time.deltaTime;
                
                // 让Kitty朝向目标
                transform.LookAt(new Vector3(targetPosition.x, transform.position.y, targetPosition.z));
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
    
    // 更新激光指向的位置
    private void UpdateLaserTargetPosition()
    {
        if (laserPointer != null)
        {
            RaycastHit hit;
            // 从激光指针位置发射射线
            if (Physics.Raycast(laserPointer.position, laserPointer.forward, out hit, 100f, laserHitLayers))
            {
                // 激光击中了物体
                laserTargetPosition = hit.point;
            }
            else
            {
                // 激光没有击中物体，使用一个默认距离
                laserTargetPosition = laserPointer.position + laserPointer.forward * 10f;
            }
        }
        else
        {
            // 如果没有激光指针，使用玩家位置
            laserTargetPosition = playerTransform.position;
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
        Debug.Log("Kitty开始跟随" + (followLaser ? "激光指针" : "玩家") + "！");
    }
    
    // 切换跟随模式
    public void ToggleFollowMode(bool followLaserMode)
    {
        followLaser = followLaserMode;
        Debug.Log("Kitty现在跟随" + (followLaser ? "激光指针" : "玩家") + "！");
    }
}