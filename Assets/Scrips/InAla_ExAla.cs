using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class InAla_ExAla : MonoBehaviour
{

        // Configuración del micrófono
        private AudioClip micClip;
        private string micDevice;
        private int sampleRate = 44100;
        private int bufferLength = 1024;

        // Variables para detección de inhalación
        public float inhalationThreshold = 0.02f; // Umbral mínimo para detectar inicio de inhalación
        public float amplitudeIncreaseRate = 0.005f; // Tasa mínima de incremento para considerar inhalación
        private float previousAmplitude = 0f;
        private bool isInhaling = false;
        private float lastInhalationTime = 0f;
        private int minBreathInterval = 1000; // milisegundos

        // Variables para detección de exhalación
        public float exhalationThreshold = 0.03f; // Umbral mínimo para detectar exhalación
        public float amplitudeDecreaseRate = 0.005f; // Tasa mínima de decremento para considerar exhalación
        private bool isExhaling = false;
        private float lastExhalationTime = 0f;

        // Eventos o acciones a realizar al detectar inhalación y exhalación
        public delegate void OnInhalationDetected();
        public event OnInhalationDetected InhalationDetected;

        public delegate void OnExhalationDetected();
        public event OnExhalationDetected ExhalationDetected;

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

                // Detección de Inhalación
                DetectInhalation(rms);

                // Detección de Exhalación
                DetectExhalation(rms);

                // Actualizar la amplitud anterior
                previousAmplitude = rms;
            }
        }

        void DetectInhalation(float currentRMS)
        {
            // Detectar si el volumen está aumentando para identificar inhalación
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
            // Detectar si el volumen está disminuyendo para identificar exhalación
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
            Debug.Log("Inhalación detectada.");
            InhalationDetected?.Invoke();
            // Aquí puedes agregar más lógica, como animaciones o interacciones en VR
        }

        void OnExhalation()
        {
            Debug.Log("Exhalación detectada.");
            ExhalationDetected?.Invoke();
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
