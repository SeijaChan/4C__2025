using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustLever : MonoBehaviour
{
    [Header("物理组件")]
    public HingeJoint hingeJoint;
    public Rigidbody leverRigidbody;

    [Header("调节参数")]
    [Range(-1f, 1f)]
    public float pivotRatio = 0.5f; // 支点位置比例（0左端，1右端）
    public float forceMultiplier = 10f;

    private Vector3 originalPivotPosition;
    private Vector3 leverLength;

    void Start()
    {
        originalPivotPosition = transform.TransformPoint(new Vector3(0, 0, 0));
        leverLength = transform.GetChild(1).position - transform.position;
    }

    void Update()
    {
        AdjustLeverPivot();
        ApplyForceInput();
    }

    void AdjustLeverPivot()
    {
        Vector3 newPosition = originalPivotPosition * pivotRatio;

        transform.GetChild(0).localPosition = newPosition - transform.position;

        hingeJoint.anchor = Vector3.zero;
        hingeJoint.connectedAnchor = newPosition;
    }

    void ApplyForceInput()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 forceDirection = (hit.point - transform.position).normalized;
                leverRigidbody.AddForceAtPosition(forceDirection * forceMultiplier, transform.position);
            }
        }
    }
}
