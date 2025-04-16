using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LinearCandleController : MonoBehaviour
{
    public float moveRange = 2.0f;
    public float sensitivity = 0.1f;
    public Transform pinhole;
    public Transform screen;
    public Transform imageCandle;

    private Vector3 _axisOrigin;
    private Vector3 _originalPosition;
    private Vector3 _originalImageSize;
    private float _currentPosition;

    private void Start()
    {
        _originalPosition = transform.position;
        _originalImageSize = imageCandle.localScale;
    }

    private void Update()
    {
        HandleInput();
        UpdateOptics();
    }

    private void HandleInput()
    {
        float input = Input.GetAxis("Horizontal");
        _currentPosition += input * sensitivity;
        _currentPosition = Mathf.Clamp(_currentPosition, -moveRange, moveRange);
        gameObject.transform.position =_originalPosition + new Vector3(_currentPosition, 0, 0);
    }

    private void UpdateOptics()
    {
        float u = Vector3.Distance(transform.position, pinhole.position);
        float v = Vector3.Distance(pinhole.position, screen.position);
        float scaleFactor = v / u;
        imageCandle.localScale = _originalImageSize * scaleFactor;
    }
}
