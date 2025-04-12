using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class MirrorUIController : MonoBehaviour
{
    public Slider rotationSlider;
    public GameObject mirror;
    public float sensitivity;


    private void Update()
    {
            mirror.transform.rotation = Quaternion.Euler(0, 0, 90 + rotationSlider.value * sensitivity);
    }
}