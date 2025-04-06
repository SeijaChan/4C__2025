using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MirrorController : MonoBehaviour
{
    public LineRenderer focusRay;
    public GameObject focusMarker;
    
    public float curvatureRadius = 2f;
    public float focalLength;
    public Vector3 focalPoint;

    public Vector2 tiltRange = new Vector2(-30, 30);
    public Vector2 panRange = new Vector2(-45, 45);
    
    public Transform sunDirection;
    public Transform combustible;

    private Vector3 _mirrorNormal;

    private void Start()
    {
        focalLength = curvatureRadius / 2;
    }

    private void Update()
    {
        focusRay.SetPosition(0,transform.position);
        focusRay.SetPosition(1,focalPoint);
        CalculateFocus();
        CheckIgnition();

        focusMarker.transform.position = focalPoint;
    }

    private void CalculateFocus()
    {
        _mirrorNormal = transform.forward;
        Vector3 sunDir = sunDirection.forward;
        float angle = Vector3.Angle(_mirrorNormal, -sunDir);

        if (angle < 5f)
        {
            focalPoint = transform.position + _mirrorNormal * focalLength;
        }
        else
        {
            focalPoint = Vector3.positiveInfinity;
        }
    }

    private void CheckIgnition()
    {
        if (Vector3.Distance(focalPoint, combustible.position) < 0.1f)
        {
            combustible.GetComponent<Combustible>().Ignite();
        }
    }

    public void AdjustTilt(float angle)
    {
        angle = Mathf.Clamp(angle,tiltRange.x,tiltRange.y);
        transform.localEulerAngles = new Vector3(angle, transform.localEulerAngles.y, 0);
    }

    public void AdjustPan(float angle)
    {
        angle = Mathf.Clamp(angle,panRange.x,panRange.y);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, angle, 0);
    }
}
