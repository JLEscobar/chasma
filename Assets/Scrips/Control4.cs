using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Control4 : MonoBehaviour
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
    public Slider sliderMantener;
    public GameObject BarraMantener;
    public GameObject Cloud1;
    public GameObject Cloud2;
    public GameObject wind;
    public bool HoldIt;
    public float  ContadorMantener;
    public bool AhoraManten;

    // Start is called before the first frame update
    void Start()
    {
        NewContador = slider.maxValue;
        Contador = 0;
        IsBreathing = false;
        SeInhalo.SetActive(false);
        Exhalar.SetActive(false);
        BarraMantener.SetActive(false);
        wind.SetActive(false);
        HoldIt =false;
        ContadorMantener = 0;
        AhoraManten = false;    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.H) && Input.GetKey(KeyCode.B) && !IsBreathing)

        {
            Contador++;
        }
        else if (!Input.GetKey(KeyCode.H) || !Input.GetKey(KeyCode.B) && !IsBreathing)

        {
            Contador = 0;
        }

        slider.value = Contador;
        sliderMantener.value = ContadorMantener;

        if (Contador >= slider.maxValue && NewContador > 0)
        {
            Text.SetActive(false);
            BarraMantener.SetActive(true);
            Cloud1.SetActive(false);
            Bar.SetActive(false);
            AhoraManten = true;
            HoldIt=true;
        }
        if (Input.GetKey(KeyCode.C)&& AhoraManten )
        {
            ContadorMantener++;
            //wind.SetActive(true);
        }
         else 
        {
            ContadorMantener = 0;
        }
        if (ContadorMantener >= sliderMantener.maxValue)
        {
            IsBreathing = true;
            BarraMantener.SetActive(false);
        }

        if (IsBreathing)
        {
            slider.value = NewContador;
            NewContador--;
            wind.SetActive(true);
            Bar.SetActive(true);
            Exhalar.SetActive(true);

        }
        if (NewContador == 0)
        {
            Exhalar.SetActive(false);
            Bar.SetActive(false);
            SeInhalo.SetActive(true);
            Cloud2.SetActive(false);
        }

    }
}
