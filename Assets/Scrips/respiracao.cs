using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class respiracao : MonoBehaviour
{
    // Configuración del micrófono
    private AudioClip micClip;
    private string micDevice;
    private int sampleRate = 44100;
    private int bufferLength = 1024;
    private int minRespirationInterval = 1000; // en milisegundos

    // Variables para detección de respiración
    public float respirationThreshold = 0.02f; // Umbral de amplitud para detectar respiración
    private float[] audioSamples;
    private float lastRespirationTime = 0f;

    // Eventos o acciones a realizar al detectar una respiración
    public delegate void OnRespirationDetected();
    public event OnRespirationDetected RespirationDetected;

    void Start()
    {
        // Verificar si hay dispositivos de micrófono disponibles
        if (Microphone.devices.Length > 0)
        {
            micDevice = Microphone.devices[0];
            // Iniciar la grabación del micrófono
            micClip = Microphone.Start(micDevice, true, 1, sampleRate);
            // Esperar hasta que el micrófono esté listo
            StartCoroutine(WaitForMicStart());
        }
        else
        {
            Debug.LogError("No se detectaron dispositivos de micrófono.");
        }
    }

    IEnumerator WaitForMicStart()
    {
        // Esperar hasta que el micrófono empiece a grabar
        while (!(Microphone.GetPosition(micDevice) > 0))
        {
            yield return null;
        }
        Debug.Log("Micrófono iniciado.");
    }

    void Update()
    {
        if (micClip != null)
        {
            // Obtener los datos de audio actuales
            audioSamples = new float[bufferLength];
            micClip.GetData(audioSamples, Microphone.GetPosition(micDevice) - bufferLength);

            // Calcular la amplitud RMS
            float rms = 0f;
            foreach (float sample in audioSamples)
            {
                rms += sample * sample;
            }
            rms = Mathf.Sqrt(rms / bufferLength);

            // Detectar respiración basado en el umbral
            if (rms > respirationThreshold)
            {
                float currentTime = Time.time * 1000f; // Convertir a milisegundos
                if (currentTime - lastRespirationTime > minRespirationInterval)
                {
                    lastRespirationTime = currentTime;
                    OnRespiration();
                }
            }
        }
    }

    void OnRespiration()
    {
        Debug.Log("Respiración detectada.");
        RespirationDetected?.Invoke();
        // Aquí puedes agregar más lógica, como animaciones o interacciones en VR
    }

    void OnDestroy()
    {
        // Detener y liberar el micrófono al destruir el objeto
        if (Microphone.IsRecording(micDevice))
        {
            Microphone.End(micDevice);
        }
    }
}