using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] Audios;
 
    public AudioSource Reproducete;
    public int AudioPosition;
    public GameObject Bruja;
    public bool NowYouCanBreath;
    public GameObject Canvas;

    void Start()
    {
        NowYouCanBreath = false;
        AudioPosition = 0;
        Canvas.SetActive(false);

        PlayNext();
    }


    private void PlayNext()
    {
        Reproducete.Stop();
        Reproducete.clip = Audios[AudioPosition];
        Reproducete.Play();
        AudioPosition++;

        if (AudioPosition == Audios.Length)
            Invoke("FinishAudios", Reproducete.clip.length);
        else
            Invoke("PlayNext", Reproducete.clip.length);
    }

    private void FinishAudios() 
    {
        Reproducete.Stop();
        Canvas.SetActive(true);
        NowYouCanBreath = true;
    }
}
