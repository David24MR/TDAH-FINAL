using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GazeWatchTracker : MonoBehaviour
{
    public GameObject slate; // El objeto que contiene el video
    public Camera userCamera; // La c�mara del usuario para el raycast

    private bool isWatching = false;
    private float watchStartTime;
    private List<float> watchIntervals = new List<float>();
    private VideoPlayer videoPlayer; // Referencia al componente VideoPlayer
    private bool videoEnded = false;
    private float videoEndTime;

    void Start()
    {
        // Obtener el componente VideoPlayer del objeto slate
        videoPlayer = slate.GetComponent<VideoPlayer>();

        // Suscribirse al evento de fin de reproducci�n del video
        videoPlayer.loopPointReached += VideoEnded;
    }

    void Update()
    {
        // Realizar un raycast desde la c�mara del usuario hacia adelante
        Ray ray = new Ray(userCamera.transform.position, userCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject == slate)
            {
                if (!isWatching)
                {
                    // Comienza a medir el tiempo
                    isWatching = true;
                    watchStartTime = Time.time;
                }
            }
            else if (isWatching)
            {
                // Deja de medir el tiempo y guarda el intervalo
                isWatching = false;
                float watchInterval = Time.time - watchStartTime;
                watchIntervals.Add(watchInterval);
            }
        }
        else if (isWatching)
        {
            // Deja de medir el tiempo y guarda el intervalo
            isWatching = false;
            float watchInterval = Time.time - watchStartTime;
            watchIntervals.Add(watchInterval);
        }

        if (videoEnded && isWatching)
        {
            // Si el video ha terminado y el usuario todav�a lo est� mirando, considerar este tiempo como el �ltimo intervalo
            float lastInterval = Time.time - videoEndTime;
            watchIntervals.Add(lastInterval);
            isWatching = false; // Reiniciar el estado de mirar despu�s de guardar el �ltimo intervalo
        }
    }

    // M�todo para manejar el evento de fin de reproducci�n del video
    private void VideoEnded(VideoPlayer vp)
    {
        if (!videoEnded)
        {
            videoEnded = true;
            videoEndTime = Time.time;

            if (isWatching)
            {
                // Si el usuario estaba mirando cuando el video termin�, guarda el intervalo hasta el momento de finalizaci�n del video
                float watchInterval = videoEndTime - watchStartTime;
                watchIntervals.Add(watchInterval);
                isWatching = false; // Reiniciar el estado de mirar despu�s de guardar el intervalo
            }
        }
    }

    // M�todo para obtener los intervalos vistos
    public List<float> GetWatchIntervals()
    {
        return watchIntervals;
    }

    // M�todo para imprimir los intervalos en consola
    public void PrintWatchIntervals()
    {
        foreach (float interval in watchIntervals)
        {
            Debug.Log("Intervalo de: " + interval + " segundos");
        }
    }
}