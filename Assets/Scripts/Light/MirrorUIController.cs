using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class MirrorUIController : MonoBehaviour
{
    public Transform mirror;
    
    public Slider tiltSlider;
    public Slider panSlider;
    public LineRenderer lightRay;
    public Button ResetButton;
    public GameObject focusIndicator;
    
    public Transform sunDirection;
    public Transform focalPoint;

    private Vector3 _initialEulerAngles;

    private void Start()
    {
        _initialEulerAngles = mirror.eulerAngles;

        tiltSlider.onValueChanged.AddListener(AdjustTilt);
        panSlider.onValueChanged.AddListener(AdjustPan);
        ResetButton.onClick.AddListener(ResetPosition);
    }

    private void Update()
    {
        UpdateLightRay();
    }

    private void AdjustTilt(float angle)
    {
        Vector3 angles = mirror.eulerAngles;
        mirror.eulerAngles = new Vector3(angle, angles.y, angles.x);
    }

    private void AdjustPan(float angle)
    {
        Vector3 angles = mirror.eulerAngles;
        mirror.eulerAngles = new Vector3(angles.x, angle, angles.z);
    }

    private void UpdateLightRay()
    {
        focusIndicator.SetActive(lightRay.enabled);
        if (lightRay.enabled)
        {
            focusIndicator.transform.position = focalPoint.position;
        }
        float pulse = Mathf.PingPong(Time.time, 1f);
        lightRay.startWidth = 0.1f + pulse * 0.05f;
        lightRay.endWidth = 0.05f + pulse * 0.03f;
        bool isValid = Vector3.Angle(mirror.forward, -sunDirection.forward) < 5f;
        lightRay.enabled = isValid;
        if (isValid)
        {
            lightRay.SetPosition(0, mirror.position);
            lightRay.SetPosition(1, focalPoint.position);
        }
    }

    private void ResetPosition()
    {
        mirror.eulerAngles = _initialEulerAngles;
        tiltSlider.value = _initialEulerAngles.x;
        panSlider.value = _initialEulerAngles.y;
    }
}