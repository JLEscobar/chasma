using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class HandGestureDetector : MonoBehaviour
{
    public bool HandsUp;
    public GameObject HandLeft;
    public GameObject Altura;
    public int Cont;
    public AudioSource Audio;
    public Control3 yue;
    public int tiempo; 
    public AudioManager AudioManager;
    public bool ReproducirAudio;
    public GameObject HandRight;
    public GameObject Canvas;
    public int TimeForEnd;
    private void Start()
    {
        ReproducirAudio = true;
        Cont = 0;  
        HandsUp = false;
        yue= yue.GetComponent<Control3>();  
        TimeForEnd  = 0;    
    }
    void Update()
    {
      if (HandLeft.transform.position.y> Altura.transform.position.y && HandRight.transform.position.y> Altura.transform.position.y&& AudioManager.NowYouCanBreath)
        {
            HandsUp = true;
            Cont = 1;
            yue.Contador ++;
        }
      if (HandLeft.transform.position.y < Altura.transform.position.y && HandRight.transform.position.y < Altura.transform.position.y && HandsUp== true)
        {
            Cont = Cont + 1;
            //yue.Contador--;
            HandsUp = false;
        }

      if (Cont >=2&& !Audio.isPlaying&&ReproducirAudio)
        {
            Audio.Play();
            Debug.Log("Daaaaaaaaaaaaaaaaaamn");
            ReproducirAudio=false;
            TimeForEnd+=1;        
        }
        Debug.Log(Cont);
        Debug.Log(yue.Contador);
        if (TimeForEnd>100)
        {
            Canvas.SetActive(false);
        }
    }



    // M�todo para ajustar el umbral en tiempo de ejecuci�n si es necesario
  
}