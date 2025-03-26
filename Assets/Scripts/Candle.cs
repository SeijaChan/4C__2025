using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Candle : MonoBehaviour
{
    [FormerlySerializedAs("pinHole")] public Transform pinhole;
    public Transform screen;
    public float realCandleHeight = 1.0f;
    public Transform imageCandle;

    private void Update()
    {
        float u = Vector3.Distance(transform.position, pinhole.position);
        float v=Vector3.Distance(pinhole.position,screen.position);
        float imageHeight=CalculateImageSize(realCandleHeight,u,v);
        imageCandle.localScale = new Vector3(imageHeight,imageHeight,imageHeight);
        Vector3 relativePos=pinhole.InverseTransformPoint(transform.position);
        imageCandle.position = pinhole.InverseTransformPoint(transform.position);
        imageCandle.position = pinhole.TransformPoint(InvertPosition(relativePos));
    }

    float CalculateImageSize(float objectHeight, float objectDistance, float screenDistance)
    {
        return objectHeight * (screenDistance / objectDistance);
    }

    Vector3 InvertPosition(Vector3 originalPosition)
    {
        return new Vector3(-originalPosition.x,-originalPosition.y,originalPosition.z);
    }
}
