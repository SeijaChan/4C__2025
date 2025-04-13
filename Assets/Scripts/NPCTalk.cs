using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using NodeCanvas.DialogueTrees;
public class NPCTalk : MonoBehaviour
{
    private bool isEntered = false; // 玩家是否进入触发器范围
    public GameObject TalkUI; // 提示 UI
    public DialogueTreeController dialogue; // 对话树控制器
    public Blackboard blackboard; // Blackboard
    private const string IS_AVAILABLE_VAR = "isAvailable"; // Blackboard 变量名称

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isEntered = true;
            TalkUI.SetActive(true); // 显示提示 UI
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isEntered = false;
            TalkUI.SetActive(false); // 隐藏提示 UI
            blackboard.SetVariableValue(IS_AVAILABLE_VAR, false); // 重置 isAvailable
        }
    }

    private void Update()
    {
        if (isEntered && Input.GetKeyDown(KeyCode.Return))
        {
            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        // 检查对话树是否已经在运行
        if (dialogue != null && !dialogue.isRunning)
        {
            // 设置 isAvailable 为 true
            blackboard.SetVariableValue(IS_AVAILABLE_VAR, true);

            // 启动对话树
            dialogue.StartDialogue();
        }
        else
        {
            Debug.LogWarning("对话树已经在运行中或未赋值！");
        }
    }
}
