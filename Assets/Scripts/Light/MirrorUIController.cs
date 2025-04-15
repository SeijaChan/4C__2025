using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class MirrorUIController : MonoBehaviour
{
    public Slider rotationSlider;
    public Text infoText;
    public GameObject lightInScene;
    public GameObject lightRay;
    public GameObject focalPoint;
    public GameObject mirror;
    public GameObject button;
    public bool canControl;
    public float sensitivity;

    private int _trigger;

    public void OnButtonClick()
    {
        _trigger++;
        switch (_trigger)
        {
            case 1: 
                lightInScene.SetActive(true); 
                lightRay.SetActive(true); 
                focalPoint.SetActive(true); 
                infoText.text = "阳燧取火：凹面镜可将阳光聚焦于焦点处产生高温";
                break;
            case 2:
                lightInScene.SetActive(false);
                infoText.text = "开始实验，左右方向键控制阳燧左右移动，滑动条控制阳燧旋转角度";
                break;
            case 3:
                button.SetActive(false);
                canControl = true;
                rotationSlider.gameObject.SetActive(true);
                break;
        }
    }

    private void Update()
    {
        if (canControl)
        {
            mirror.transform.rotation = Quaternion.Euler(0, 0, 90 + rotationSlider.value * sensitivity);
        }
    }
}