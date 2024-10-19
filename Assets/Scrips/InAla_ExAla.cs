using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class InAla_ExAla : MonoBehaviour
{

        // Configuraci�n del micr�fono
        private AudioClip micClip;
        private string micDevice;
        private int sampleRate = 44100;
        private int bufferLength = 1024;

        // Variables para detecci�n de inhalaci�n
        public float inhalationThreshold = 0.02f; // Umbral m�nimo para detectar inicio de inhalaci�n
        public float amplitudeIncreaseRate = 0.005f; // Tasa m�nima de incremento para considerar inhalaci�n
        private float previousAmplitude = 0f;
        private bool isInhaling = false;
        private float lastInhalationTime = 0f;
        private int minBreathInterval = 1000; // milisegundos

        // Variables para detecci�n de exhalaci�n
        public float exhalationThreshold = 0.03f; // Umbral m�nimo para detectar exhalaci�n
        public float amplitudeDecreaseRate = 0.005f; // Tasa m�nima de decremento para considerar exhalaci�n
        private bool isExhaling = false;
        private float lastExhalationTime = 0f;

        // Eventos o acciones a realizar al detectar inhalaci�n y exhalaci�n
        public delegate void OnInhalationDetected();
        public event OnInhalationDetected InhalationDetected;

        public delegate void OnExhalationDetected();
        public event OnExhalationDetected ExhalationDetected;

        void Start()
        {
            // Verificar si hay dispositivos de micr�fono disponibles
            if (Microphone.devices.Length > 0)
            {
                micDevice = Microphone.devices[0];
                // Iniciar la grabaci�n del micr�fono
                micClip = Microphone.Start(micDevice, true, 1, sampleRate);
                // Esperar hasta que el micr�fono est� listo
                StartCoroutine(WaitForMicStart());
            }
            else
            {
                Debug.LogError("No se detectaron dispositivos de micr�fono.");
            }
        }

        IEnumerator WaitForMicStart()
        {
            // Esperar hasta que el micr�fono empiece a grabar
            while (!(Microphone.GetPosition(micDevice) > 0))
            {
                yield return null;
            }
            Debug.Log("Micr�fono iniciado.");
        }

        void Update()
        {
            if (micClip != null)
            {
                // Obtener los datos de audio actuales
                float[] audioSamples = new float[bufferLength];
                int micPosition = Microphone.GetPosition(micDevice) - bufferLength;
                if (micPosition < 0) micPosition = 0;
                micClip.GetData(audioSamples, micPosition);

                // Calcular la amplitud RMS
                float rms = 0f;
                foreach (float sample in audioSamples)
                {
                    rms += sample * sample;
                }
                rms = Mathf.Sqrt(rms / bufferLength);

                // Detecci�n de Inhalaci�n
                DetectInhalation(rms);

                // Detecci�n de Exhalaci�n
                DetectExhalation(rms);

                // Actualizar la amplitud anterior
                previousAmplitude = rms;
            }
        }

        void DetectInhalation(float currentRMS)
        {
            // Detectar si el volumen est� aumentando para identificar inhalaci�n
            if (currentRMS > inhalationThreshold && (currentRMS - previousAmplitude) > amplitudeIncreaseRate)
            {
                if (!isInhaling)
                {
                    isInhaling = true;
                    float currentTime = Time.time * 1000f; // Convertir a milisegundos
                    if (currentTime - lastInhalationTime > minBreathInterval)
                    {
                        lastInhalationTime = currentTime;
                        OnInhalation();
                    }
                }
            }
            else if (currentRMS <= previousAmplitude) // Si el volumen no sigue subiendo, marcar como no inhalando
            {
                isInhaling = false;
            }
        }

        void DetectExhalation(float currentRMS)
        {
            // Detectar si el volumen est� disminuyendo para identificar exhalaci�n
            if (currentRMS > exhalationThreshold && (previousAmplitude - currentRMS) > amplitudeDecreaseRate)
            {
                if (!isExhaling)
                {
                    isExhaling = true;
                    float currentTime = Time.time * 1000f; // Convertir a milisegundos
                    if (currentTime - lastExhalationTime > minBreathInterval)
                    {
                        lastExhalationTime = currentTime;
                        OnExhalation();
                    }
                }
            }
            else if (currentRMS >= previousAmplitude) // Si el volumen no sigue bajando, marcar como no exhalando
            {
                isExhaling = false;
            }
        }

        void OnInhalation()
        {   
            Debug.Log("Inhalaci�n detectada.");
            InhalationDetected?.Invoke();
            // Aqu� puedes agregar m�s l�gica, como animaciones o interacciones en VR
        }

        void OnExhalation()
        {
            Debug.Log("Exhalaci�n detectada.");
            ExhalationDetected?.Invoke();
            // Aqu� puedes agregar m�s l�gica, como animaciones o interacciones en VR
        }

        void OnDestroy()
        {
            // Detener y liberar el micr�fono al destruir el objeto
            if (Microphone.IsRecording(micDevice))
            {
                Microphone.End(micDevice);
            }
        }
}
