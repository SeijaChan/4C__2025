using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroMirror : MonoBehaviour
{
    public Transform[] lights;
    public Transform focalPoint;
    public LineRenderer lightRenderer;
    public float limit;
    
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

        foreach (var lightRay in lights)
        {
            if (lightRay.position.y - transform.position.y < limit &&
                lightRay.position.y - transform.position.y > -limit)
            {
                _isShoot = true;
                break;
            }
            _isShoot = false;
            lightRenderer.enabled = false;
        }
    }
}
