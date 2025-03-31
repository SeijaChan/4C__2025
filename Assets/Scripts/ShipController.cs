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

    private float _temp;
    private void Start()
    {
        _shipRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isFlowBegin && hasBulkheads)
        {
            _temp += rotateSpeed * Time.deltaTime;
            rotateAngle = Mathf.Clamp(_temp, 0, tiltLimit);
            _shipRb.rotation = Quaternion.Euler(0, 0, -rotateAngle);
        }

        if (isFlowBegin && !hasBulkheads)
        {
            rotateAngle += rotateSpeed * Time.deltaTime;
            _shipRb.rotation = Quaternion.Euler(0, 0, -rotateAngle);
            if (rotateAngle > tiltLimit)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void ResetShip()
    {
        gameObject.SetActive(true);
        _temp = 0;
        isFlowBegin = false;
        rotateAngle = 0;
        _shipRb.rotation = Quaternion.Euler(rotateAngle, 0, 0);
    }

    public void FlowBegin()
    {
        isFlowBegin = true;
    }
}
