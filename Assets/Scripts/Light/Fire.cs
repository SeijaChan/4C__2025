using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private Animator _animator;
    private float _timer;
    private bool _isFire;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isFire)
        {
            _timer += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Light"))
        {
            _isFire = true;
            _animator.SetTrigger("Enter");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Light"))
        {
            _isFire = false;
            if(_timer < 2)
            {
                _animator.SetTrigger("Exit");
            }
            _timer = 0;
        }
    }
}
