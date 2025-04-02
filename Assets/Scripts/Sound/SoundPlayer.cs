using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public float speed;
    public int health;
    private int _currentHealth;
    private bool _inLeftArea;
    private bool _inRightArea;
    
    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        transform.Translate(h * Time.deltaTime * speed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftArea"))
        {
            _inLeftArea = true;
        }

        if (other.CompareTag("RightArea"))
        {
            _inRightArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LeftArea"))
        {
            _inLeftArea = false;
        }

        if (other.CompareTag("RightArea"))
        {
            _inRightArea = false;
        }
    }

    public bool CheckAArea(bool isLeft)
    {
        return isLeft ? _inLeftArea : _inRightArea;
    }
}
