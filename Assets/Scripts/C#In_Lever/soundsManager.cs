using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class soundsManager : MonoBehaviour
{
    public Sounds[] musicSounds, SfxSounds;
    public AudioSource musicSource, SfxSource;

    public static soundsManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        PlayMusic("Bgm");
    }
    public void PlayMusic(string name)
    {
        Sounds s = Array.Find(musicSounds, x => x.name == name);
        if (s != null)
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void SfxPlay(string name)
    {
        Sounds s = Array.Find(SfxSounds, x => x.name == name);
        if (s != null)
        {
            SfxSource.PlayOneShot(s.clip);
        }
    }
}
