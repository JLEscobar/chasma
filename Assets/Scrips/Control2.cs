using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Control2 : MonoBehaviour
{
    public Slider slider;
    public int Contador;
    public bool IsBreathing;
    public GameObject Bar;
    public GameObject SeInhalo;
    public GameObject wind;
    // Start is called before the first frame update
    void Start()
    {
        Contador = 0;
        IsBreathing = false;
        SeInhalo.SetActive(false);
        wind.SetActive(false);

    }

    // Update is called once per frame
    void  FixedUpdate()
    {
         if (Input.GetKey(KeyCode.C))
        {
            Contador++;
        }
        else
        {
            Contador=0;
        }
            slider.value = Contador;
        
        
        if (Contador >= 500)
        {
            Bar.SetActive(false);
            SeInhalo.SetActive(true);
            wind.SetActive(true);
        }
    }
}
