using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CambioEscena : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            SceneManager.LoadScene("Pokemon");
        }
        if (Input.GetKey(KeyCode.W))
        {
            SceneManager.LoadScene("InhalaYOprimeBoton");
        }
        if (Input.GetKey(KeyCode.E))
        {
            SceneManager.LoadScene("MantenerRespiración");
        }
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("escena4");
        }
    }
}
