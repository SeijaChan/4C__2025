using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [Header("References")]
    public Transform[] snapPoints; // 吸附点数组
    public Transform povit;  // 需要拖动的子物体
    public float sensitivity = 0.01f; // 鼠标灵敏度

    [Header("Visual Settings")]
    public Color highlightedColor = new Color(1, 1, 1, 0.5f); // 拖动时的半透明颜色
    public Color normalColor = Color.white; // 默认颜色

    private Renderer _childRenderer;
    private bool _isDragging;
    private Vector3 _startPosition;
    private float _startMouseX;
    private Material _highlightedMaterial;
    private Material _originalMaterial;

    void Start()
    {
        // 获取子物体渲染器并初始化材质
        _childRenderer = povit.GetComponent<Renderer>();
        if (_childRenderer == null)
        {
            return;
        }

        // 创建材质副本以避免影响原始材质
        _originalMaterial = new Material(_childRenderer.material);
        _highlightedMaterial = new Material(_originalMaterial);
        _highlightedMaterial.color = highlightedColor;
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        // 检测鼠标按下事件
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // 检查是否点击在子物体上
                if (hit.transform == povit)
                {
                    Vector3 vector3 = povit.transform.position;
                    StartDrag(vector3);
                }
            }
        }

        // 拖动中更新位置
        if (_isDragging)
        {
            Drag();
        }

        // 释放鼠标时吸附并停止拖动
        if (Input.GetMouseButtonUp(0) && _isDragging)
        {
            StopDrag();
        }
    }

    void StartDrag(Vector3 startPositionWorld)
    {
        _isDragging = true;
        _startPosition = startPositionWorld;
        _startMouseX = Input.mousePosition.x;
        _childRenderer.material = _highlightedMaterial;
    }

    void Drag()
    {
        // 计算鼠标横向移动量
        float deltaX = (Input.mousePosition.x - _startMouseX) * sensitivity;

        // 保持Y/Z轴不变，仅更新X轴位置
        if(_startPosition.x + deltaX >= snapPoints[0].transform.position.x && _startPosition.x + deltaX <= snapPoints[2].transform.position.x){
            Vector3 newPosition = new Vector3(
                _startPosition.x + deltaX,
                _startPosition.y,
                _startPosition.z
            );

            povit.position = newPosition; }
    }

    void StopDrag()
    {
        _isDragging = false;
        _childRenderer.material = _originalMaterial;
        SnapToNearest();
    }

    void SnapToNearest()
    {
        if (snapPoints == null || snapPoints.Length == 0) return;

        Transform nearestSnap = null;
        float minDistance = float.MaxValue;

        // 遍历所有吸附点寻找最近的一个
        foreach (Transform snap in snapPoints)
        {
            float distance = Vector3.Distance(povit.position, snap.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestSnap = snap;
            }
        }

        // 如果找到最近的吸附点则吸附
        if (nearestSnap != null)
        {
            povit.position = nearestSnap.position;
        }
    }
}
