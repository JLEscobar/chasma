using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class HandGestureDetector : MonoBehaviour
{
    public bool HandsUp;
    public GameObject Hands;
    public GameObject Altura;
    public int Cont;
    public AudioSource Audio;
    public Control3 yue;
    public int tiempo; 
    private void Start()
    {

        Cont = 0;  
        HandsUp = false;
        yue= yue.GetComponent<Control3>();  

    }
    void Update()
    {
      if (Hands.transform.position.y> Altura.transform.position.y)
        {
            HandsUp = true;
            Cont = 1;
            yue.Contador ++;
        }
      if (Hands.transform.position.y < Altura.transform.position.y && HandsUp== true)
        {
            Cont = Cont + 1;
            //yue.Contador--;
            HandsUp = false;
        }

      if (Cont >=2&& !Audio.isPlaying)
        {
            Audio.Play();
        }
        Debug.Log(Cont);
        Debug.Log(yue.Contador);
    }



    // Método para ajustar el umbral en tiempo de ejecución si es necesario
  
}