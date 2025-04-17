using UnityEngine;
using UnityEngine.UI;

public class Item_Manager : MonoBehaviour
{
    [Header("UI Settings")]
    [Tooltip("The UI element to show when player is near")]
    public GameObject interactionPrompt; // 拖拽你的UI提示对象到这里

    [Header("State Tracking")]
    [Tooltip("Tracks whether player is currently in the trigger zone")]
    public bool isEntered = false; // 新增的布尔变量

    private void Start()
    {
        // 确保开始时UI提示是隐藏的
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Interaction prompt UI is not assigned in the inspector.");
        }

        isEntered = false; // 初始化状态
    }

    private void OnTriggerEnter(Collider other)
    {
        // 检查碰撞对象是否是玩家
        if (other.CompareTag("Player"))
        {
            // 设置进入状态为真
            isEntered = true;

            // 显示交互提示
            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 当玩家离开时隐藏提示
        if (other.CompareTag("Player"))
        {
            // 设置进入状态为假
            isEntered = false;

            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(false);
            }
        }
    }

    // 可选：添加一个公共方法获取当前状态
    public bool IsPlayerInTriggerZone()
    {
        return isEntered;
    }
}