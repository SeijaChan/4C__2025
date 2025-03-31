using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LinearCandleController : MonoBehaviour
{
    public Transform movementAxis;
    public float moveRange = 2.0f;
    public float sensitivity = 0.1f;
    public Transform pinhole;
    public Transform screen;
    public float realHeight = 1.0f;
    public Transform imageCandle;

    private Vector3 axisOrigin;
    private Vector3 originalPosition;
    private Vector3 originalImageSize;
    private float currentPosition;

    private void Start()
    {
        originalPosition = transform.position;
        originalImageSize = imageCandle.localScale;
    }

    private void Update()
    {
        HandleInput();
        UpdateOptics();
    }

    private void HandleInput()
    {
        float input = Input.GetAxis("Horizontal");
        currentPosition += input * sensitivity;
        currentPosition = Mathf.Clamp(currentPosition, -moveRange, moveRange);
        gameObject.transform.position =originalPosition + new Vector3(currentPosition, 0, 0);
    }

    private void UpdateOptics()
    {
        float u = Vector3.Distance(transform.position, pinhole.position);
        float v = Vector3.Distance(pinhole.position, screen.position);
        float scaleFactor = u / v;
        imageCandle.localScale = originalImageSize * (scaleFactor * realHeight);
    }
}
