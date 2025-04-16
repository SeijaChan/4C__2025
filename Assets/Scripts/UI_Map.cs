using UnityEngine;
using NodeCanvas.Framework;

[RequireComponent(typeof(Blackboard))]
public class UI_Map : MonoBehaviour
{
    [Header("Blackboard 控制的UI")]
    public GameObject temporaryUI;  // 原先的UI（isBookGot触发）
    public GameObject EndUI;

    public float autoHideTime = 1f; // 自动隐藏时间

    [Header("ESC键控制的UI")]
    public GameObject bookUI;       // 书本UI（ESC键切换）

    private Blackboard blackboard;
    private Coroutine hideCoroutine;
    private bool lastBookGotState;

    void Awake()
    {
        blackboard = GetComponent<Blackboard>();
        SetBookUIVisible(false);    // 初始隐藏书本UI
    }

    void Update()
    {
        // ESC键切换书本UI
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleBookUI();
        }

        // 检测Blackboard变量变化
        var variable = blackboard.GetVariable<bool>("isBookGot");
        var variable2 = blackboard.GetVariable<bool>("isEnded");
        if (variable != null)
        {
            bool currentState = variable.value;
            if (currentState && !lastBookGotState)
            {
                ShowTemporaryUI();
            }
            lastBookGotState = currentState;
        }
        if (variable2.value == true)
        {
            EndUI.SetActive(true);   
        }

        
    }

    // ======== 书本UI控制 ========
    private void ToggleBookUI()
    {
        SetBookUIVisible(!bookUI.activeSelf);
    }

    private void SetBookUIVisible(bool visible)
    {
        if (bookUI != null)
            bookUI.SetActive(visible);
    }

    // ======== 临时UI控制 ========
    private void ShowTemporaryUI()
    {
        if (temporaryUI == null) return;

        // 显示UI
        temporaryUI.SetActive(true);

        // 取消之前的隐藏协程
        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);

        // 启动新协程
        hideCoroutine = StartCoroutine(HideAfterDelay());
    }

    private System.Collections.IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(autoHideTime);
        if (temporaryUI != null)
            temporaryUI.SetActive(false);
        hideCoroutine = null;
    }
}