using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Control1 : MonoBehaviour
{
    public Slider slider;
    public int Contador;
    public bool IsBreathing;
    public GameObject Bar;
    public GameObject SeInhalo;
    // Start is called before the first frame update
     void Start()
    {
        Contador = 0;   
        IsBreathing = false;
        SeInhalo.SetActive(false);
        
    }

    // Update is called once per frame
    void  FixedUpdate()
    {
        if (Input.GetKey(KeyCode.H)&& Input.GetKey(KeyCode.B)) 
        
        {
                 Contador++;    
        }
        else 
        {
            Contador = 0;
        }

        slider.value = Contador;    
        
        if (Contador>=250)
        {
           Bar.SetActive(false);
           SeInhalo.SetActive(true);
        }
    }
}
