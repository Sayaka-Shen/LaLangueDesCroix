using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] musicSound;
    public Sound[] sfxSound;
    public AudioSource musicSource;
    public AudioSource sfxSource;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSound, sound => sound.name == name);
        if(s == null)
        {
            Debug.Log("Sound not found: " + name);
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
   
    }
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSound, sound => sound.name == name);

        if(s == null)
        {
            Debug.Log("Sound not found: " + name);
        }
        else
        {
            sfxSource.clip = s.clip;
            sfxSource.Play();
        }
   
    }
}
