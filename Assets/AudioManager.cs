using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] Audios;
    public AudioClip Audio1;
    public AudioClip Audio2;
    public AudioClip Audio3;
    public AudioClip Audio4;
    public AudioClip Audio5;
    public AudioSource Reproducete;
    public int AudioPosition;
    public GameObject Bruja;

    void Start()
    {
        AudioPosition = 0;

        Audios = new AudioClip[5];
        Audios[1] = Audio1;
        Audios[2] = Audio2;
        Audios[3] = Audio3;
        Audios[4] = Audio4;

       
    }

    private void FixedUpdate()
    {
        if (Bruja.activeInHierarchy)
        {
            if (!Reproducete.isPlaying)
            {
                if (AudioPosition < Audios.Length - 1)
                {
                    Reproducete.Stop();
                    AudioPosition++;
                    Reproducete.clip = Audios[AudioPosition];
                    Reproducete.Play();
                }
                else
                {
                    Reproducete.Stop(); 
                }
            }
        }
    }
}
