using System.Collections.Generic;
using UnityEngine;

public class GazeWatchTracker : MonoBehaviour
{
    public GameObject slate; // El objeto que contiene el video
    public Camera userCamera; // La cámara del usuario para el raycast

    private bool isWatching = false;
    private float watchStartTime;
    private List<float> watchIntervals = new List<float>();

    void Update()
    {
        // Realizar un raycast desde la cámara del usuario hacia adelante
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
    }

    // Método para obtener los intervalos vistos
    public List<float> GetWatchIntervals()
    {
        return watchIntervals;
    }

    // Método para imprimir los intervalos en consola
    public void PrintWatchIntervals()
    {
        foreach (float interval in watchIntervals)
        {
            Debug.Log("Intervalo visto: " + interval + " segundos");
        }
    }
}