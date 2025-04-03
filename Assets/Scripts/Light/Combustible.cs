using System;
using UnityEngine;

public class Combustible : MonoBehaviour
{
    public ParticleSystem fireEffect;
    public float igniteTime = 3f;

    private float _heatAccumulation;

    public void Ignite()
    {
        _heatAccumulation += Time.deltaTime;
        if (_heatAccumulation > igniteTime && !fireEffect.isPlaying)
        {
            fireEffect.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _heatAccumulation = 0;
        if (fireEffect.isPlaying)
        {
            fireEffect.Stop();
        }
    }
}