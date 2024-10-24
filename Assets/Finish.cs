using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public int time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        time++;
        if (time >500)
        {
            this.gameObject.SetActive(false);
        }
    }
  
}
