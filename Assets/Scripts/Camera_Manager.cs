using UnityEngine;

public class Camera_Manager : MonoBehaviour
{
    [Header("Camera Settings")]
    [Tooltip("主摄像头（默认视角）")]
    public Camera mainCamera;

    [Tooltip("要切换到的次要摄像头")]
    public Camera secondaryCamera;

    [Header("交互设置")]
    [Tooltip("拖拽带有 Item_Manager 脚本的游戏对象到这里")]
    public Item_Manager itemManager;

    [Tooltip("摄像头切换过渡时间（秒）")]
    public float transitionDuration = 1.0f;

    private bool isSecondaryActive = false;
    private float transitionProgress = 0f;
    private bool isTransitioning = false;

    private void Start()
    {
        // 初始化摄像头状态
        SetCameraState(mainCamera, true);
        SetCameraState(secondaryCamera, false);
    }

    private void Update()
    {
        // 检查是否在交互区域内且按下回车键
        if (CanSwitchCamera() && Input.GetKeyDown(KeyCode.Return))
        {
            StartCameraTransition();
        }

        // 处理摄像头过渡效果
        if (isTransitioning)
        {
            UpdateCameraTransition();
        }
    }

    private bool CanSwitchCamera()
    {
        return itemManager != null && itemManager.isEntered;
    }

    private void StartCameraTransition()
    {
        isTransitioning = true;
        transitionProgress = 0f;
        isSecondaryActive = !isSecondaryActive;
    }

    private void UpdateCameraTransition()
    {
        transitionProgress += Time.deltaTime / transitionDuration;

        if (transitionProgress >= 1f)
        {
            transitionProgress = 1f;
            isTransitioning = false;
        }

        // 计算插值权重
        float mainWeight = isSecondaryActive ? 1 - transitionProgress : transitionProgress;
        float secondaryWeight = isSecondaryActive ? transitionProgress : 1 - transitionProgress;

        // 设置摄像头优先级（这里使用简单的启用/禁用方式）
        // 更高级的实现可以使用Camera权重或渲染纹理混合
        SetCameraState(mainCamera, mainWeight > 0.5f);
        SetCameraState(secondaryCamera, secondaryWeight > 0.5f);

        // 处理音频监听器（确保只有一个激活）
        mainCamera.GetComponent<AudioListener>().enabled = mainWeight > 0.5f;
        secondaryCamera.GetComponent<AudioListener>().enabled = secondaryWeight > 0.5f;
    }

    private void SetCameraState(Camera cam, bool active)
    {
        if (cam != null)
        {
            cam.enabled = active;
        }
    }

    // 公共方法供其他脚本查询当前摄像头状态
    public bool IsUsingSecondaryCamera()
    {
        return isSecondaryActive;
    }
}