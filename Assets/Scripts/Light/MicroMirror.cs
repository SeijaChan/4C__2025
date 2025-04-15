using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroMirror : MonoBehaviour
{
    public Transform focalPoint;
    public LineRenderer lightRenderer;
    private bool _isShoot;

    private void Update()
    {
        ShowLightRay();
    }

    private void ShowLightRay()
    {
        if (_isShoot)
        {
            lightRenderer.enabled = true;
            lightRenderer.SetPosition(0,transform.position);
            lightRenderer.SetPosition(1, focalPoint.position);
        }
        else
        {
            lightRenderer.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Light"))
        {
            _isShoot = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Light"))
        {
            _isShoot = false;
        }
    }
}
