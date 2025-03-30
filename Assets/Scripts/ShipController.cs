using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public bool isFlowBegin;
    public float rotateAngle;
    public bool hasBulkheads = true;
    public float rotateSpeed;
    public float tiltLimit = 30f;
    private Rigidbody _shipRb;

    private void Start()
    {
        _shipRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isFlowBegin && hasBulkheads)
        {
            rotateAngle = Mathf.Clamp(rotateSpeed * Time.deltaTime, 0, tiltLimit);
            _shipRb.rotation = Quaternion.Euler(rotateAngle, 0, 0);
        }

        if (isFlowBegin && !hasBulkheads)
        {
            rotateAngle += rotateSpeed * Time.deltaTime;
            _shipRb.rotation = Quaternion.Euler(rotateAngle, 0, 0);
            if (rotateAngle > tiltLimit)
            {
                Destroy(gameObject);
            }
        }
    }

    public void ResetShip()
    {
        isFlowBegin = false;
        rotateAngle = rotateSpeed = 0;
        _shipRb.rotation = Quaternion.Euler(rotateAngle, 0, 0);
    }

    public void FlowBegin()
    {
        isFlowBegin = true;
    }
}
