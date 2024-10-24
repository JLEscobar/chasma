using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Control3 : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public int Contador;
    public bool IsBreathing;
    public float NewContador;
    public GameObject Bar;
    public GameObject Text;
    public GameObject SeInhalo;
    public GameObject Exhalar;
    public GameObject Canvas;
    public GameObject CanvaFInal;
    public int TimeForEnd;
    // Start is called before the first frame update
    void Start()
    {
        NewContador = slider.maxValue;
        Contador = 0;
        IsBreathing = false;
        SeInhalo.SetActive(false);
        Exhalar.SetActive(false);
        TimeForEnd = 0;       
        CanvaFInal.SetActive(true);    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      /* if (Input.GetKey(KeyCode.H) && Input.GetKey(KeyCode.B) && !IsBreathing)

        {
            Contador++;
        }
        else if (!Input.GetKey(KeyCode.H) || !Input.GetKey(KeyCode.B) && !IsBreathing)

        {
            Contador = 0;
        }*/

        slider.value = Contador;
         
        if (Contador >= slider.maxValue&&NewContador>0)
        {
            Text.SetActive(false);
            Exhalar.SetActive(true);
            IsBreathing=true;
            

        }
        if (IsBreathing)
        {
            slider.value = NewContador;
            NewContador--;
           
        }
        if (NewContador==0)
        {
            Exhalar.SetActive(false);
            SeInhalo.SetActive(true);
            Bar.SetActive(false);
            TimeForEnd++;
        }
        if (NewContador<0)
        {
            TimeForEnd++;
        }
        if (TimeForEnd>100)
        {
            CanvaFInal.SetActive(false);    
        }
 
        
    }
}

