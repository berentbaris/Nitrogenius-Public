using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
            CreateSoundInstances();
            instance.sounds[7].PlaySound();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void CreateSoundInstances()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
        }
    }
}