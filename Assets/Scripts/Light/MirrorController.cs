using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MirrorController : MonoBehaviour
{
    private float _horizon;
    public float sensitivity;
    public MirrorUIController mirrorUI;
    
    private void Update()
    {   
        if(mirrorUI.canControl)
        {
            Move();
        }
    }

    private void Move()
    {
        _horizon = Input.GetAxis("Horizontal") * sensitivity;
        transform.position += new Vector3(_horizon, 0, 0) * Time.deltaTime;
    }
}
