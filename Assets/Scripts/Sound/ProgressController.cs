using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressController : MonoBehaviour
{
    public Image progressImage;
    public AudioSource musicSource;

    private void Update()
    {
        if (musicSource.isPlaying)
        {
            float progress = musicSource.time / musicSource.clip.length;
            progressImage.fillAmount = progress;
        }
    }
}
