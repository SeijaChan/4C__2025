using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRay : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    public GameObject endPoint;
    public GameObject hole;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        _lineRenderer.SetPosition(0,transform.position);
        _lineRenderer.SetPosition(1, hole.transform.position);
        _lineRenderer.SetPosition(2, endPoint.transform.position);
    }
}
