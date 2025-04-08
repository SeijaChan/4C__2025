using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MirrorController : MonoBehaviour
{
    private float _horizon;
    private float _vertical;
    public float sensitivity;
    
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _horizon = Input.GetAxis("Horizontal") * sensitivity;
        _vertical = Input.GetAxis("Vertical") * sensitivity;
        transform.Translate(Vector3.up * (-_horizon * Time.deltaTime));
        transform.Translate(Vector3.right * (_vertical * Time.deltaTime));
    }
}
