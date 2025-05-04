using UnityEngine;
using UnityEngine.XR;

public class KittyFollowLaser : MonoBehaviour
{
    // 激光指示器相关
    public Transform laserPointer; // 激光指示器的Transform
    private Vector3 targetPosition; // 激光指向的目标位置
    
    public float followSpeed = 1.0f; // 显著增加默认速度
    public float stopDistance = 0.5f; // 到达目标点的停止距离
    private bool shouldFollow = true; // 默认设置为true，让猫咪自动跟随
    
    // 动画参数名称
    private readonly string walkAnimParam = "IsWalking";
    // 直接引用Animator组件
    public Animator kittyAnimator;
    
    // 添加音频相关变量
    private AudioSource catMeowAudio;
    private float soundTimer = 0f;
    public float soundInterval = 10f; // 音频播放间隔，默认10秒
    
    // 添加调试变量
    private float debugTimer = 0f;
    public float debugInterval = 1f; // 每秒打印一次位置信息

    private void Start()
    {
        // 尝试获取猫叫声音频源
        catMeowAudio = transform.Find("cat_meow")?.GetComponent<AudioSource>();
        if (catMeowAudio == null)
        {
            catMeowAudio = GetComponent<AudioSource>();
        }
        
        // 注意：不再使用标签查找，需要在Inspector中手动指定laserPointer
        if (laserPointer == null)
        {
            Debug.LogWarning("未指定激光指示器，请在Inspector中设置laserPointer引用！");
        }
        else
        {
            // 自动开始跟随
            StartFollowing();
        }
    }

    private void Update()
    {
        // 调试输出激光位置信息
        debugTimer += Time.deltaTime;
        if (debugTimer >= debugInterval && laserPointer != null)
        {
            UpdateLaserTargetPosition(); // 更新目标位置
            // Debug.Log($"激光指向位置: {targetPosition}, 猫咪位置: {transform.position}, 距离: {Vector3.Distance(transform.position, targetPosition)}, 是否跟随: {shouldFollow}");
            debugTimer = 0f;
        }
        
        if (shouldFollow && laserPointer != null)
        {
            // 更新目标位置 - 使用射线检测获取激光指向的位置
            UpdateLaserTargetPosition();
            
            // 更新音频计时器
            soundTimer += Time.deltaTime;
            
            // 每隔指定时间播放一次猫叫声
            if (soundTimer >= soundInterval && catMeowAudio != null)
            {
                catMeowAudio.Play();
                soundTimer = 0f; // 重置计时器
            }
            
            // 计算与目标位置的距离
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
                // 只在水平方向上移动
                direction.y = 0;
                direction = direction.normalized;
                
                // 使用更明显的移动速度
                float moveStep = followSpeed * Time.deltaTime;
                transform.position += direction * moveStep;
                
                // 让Kitty朝向目标位置
                transform.LookAt(new Vector3(targetPosition.x, transform.position.y, targetPosition.z));
                
                // 添加调试输出
                // Debug.Log($"猫咪正在移动! 方向: {direction}, 步长: {moveStep}, 新位置: {transform.position}");
            }
            else
            {
                // 停止行走动画
                if (kittyAnimator != null)
                {
                    kittyAnimator.SetBool(walkAnimParam, false);
                }
                // Debug.Log("猫咪已到达目标位置，停止移动");
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
    
    // 更新激光指向的目标位置
    private void UpdateLaserTargetPosition()
    {
        if (laserPointer != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(laserPointer.position, laserPointer.forward, out hit))
            {
                // 激光击中了物体，使用击中点作为目标
                targetPosition = hit.point;
                // Debug.Log($"激光射线击中: {hit.collider.name}, 位置: {hit.point}");
            }
            else
            {
                // 激光没有击中物体，使用一个远处的点作为目标
                targetPosition = laserPointer.position + laserPointer.forward * 100f;
                // Debug.Log("激光射线未击中任何物体，使用远处点");
            }
            
            // 确保目标位置在地面上（可选）
            targetPosition.y = transform.position.y;
        }
    }

    public void StartFollowing()
    {
        if (laserPointer == null)
        {
            // Debug.LogError("无法找到激光指示器！");
            return;
        }
        shouldFollow = true;
        soundTimer = soundInterval; // 设置计时器，使猫咪开始跟随时立即发出叫声
        // Debug.Log("Kitty开始跟随激光点！");
    }
}