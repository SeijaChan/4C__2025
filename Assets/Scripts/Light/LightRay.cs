using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRay : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    public GameObject endPoint;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        _lineRenderer.SetPosition(0,transform.position);
        _lineRenderer.SetPosition(1, endPoint.transform.position);
    }
}
