using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections.Generic;
using MixedReality.Toolkit.UX;

public class EyeInteractable : MonoBehaviour
{
    public float maxGazeDistance = 10f; // Distancia m�xima de detecci�n de la mirada
    public LayerMask gazeLayerMask;
    public VideoPlayer videoPlayer; // Arrastra aqu� el componente VideoPlayer desde el inspector
    public PressableButton pressableButton; // Arrastra aqu� el PressableButton desde el inspector
    public Text gazeInfoText; // Objeto de texto para mostrar la informaci�n de mirada
    public List<GameObject> allFigures; // Lista de todas las figuras que se deben ocultar al finalizar el video
    public GameObject resultsCanvas; // Canvas de los resultados de los intervalos de mirada

    private bool isBeingGazed = false;
    private float gazeStartTime = 0f;
    private List<(float, float)> gazeIntervals = new List<(float, float)>(); // Lista de intervalos de mirada
    private bool isVideoPlaying = false;
    private int clickCount = 0;
    private float totalGazeTime = 0f; // Tiempo total de mirada al video

    private void Start()
    {
        // A�adir un listener al PressableButton para ejecutar la funci�n CountButtonPresses cuando se haga clic
        if (pressableButton != null)
        {
            pressableButton.OnClicked.AddListener(CountButtonPresses);
        }
        else
        {
            Debug.LogError("No se encontr� un componente PressableButton en los hijos de este objeto.");
        }

        // A�adir listeners al VideoPlayer para detectar cuando el video comienza y termina
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
            videoPlayer.started += OnVideoStart;
            videoPlayer.playOnAwake = false; // Aseg�rate de que el video no se reproduzca autom�ticamente al iniciar la escena
        }
    }

    private void Update()
    {
        if (isVideoPlaying)
        {
            // Emitir el rayo desde la posici�n de la c�mara hacia adelante
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, maxGazeDistance, gazeLayerMask))
            {
                // Verificar si el objeto detectado es el video o el bot�n
                if (hit.collider != null && (hit.collider.gameObject == gameObject || hit.collider.gameObject == pressableButton.gameObject))
                {
                    if (!isBeingGazed)
                    {
                        isBeingGazed = true;
                        gazeStartTime = Time.time; // Registrar el inicio del intervalo de mirada
                    }
                }
                else
                {
                    if (isBeingGazed)
                    {
                        float gazeEndTime = Time.time; // Registrar el final del intervalo de mirada
                        totalGazeTime += gazeEndTime - gazeStartTime; // Sumar al tiempo total de mirada
                        gazeIntervals.Add((gazeStartTime, gazeEndTime)); // Agregar el intervalo a la lista
                        isBeingGazed = false;
                    }
                }
            }
            else
            {
                if (isBeingGazed)
                {
                    float gazeEndTime = Time.time; // Registrar el final del intervalo de mirada
                    totalGazeTime += gazeEndTime - gazeStartTime; // Sumar al tiempo total de mirada
                    gazeIntervals.Add((gazeStartTime, gazeEndTime)); // Agregar el intervalo a la lista
                    isBeingGazed = false;
                }
            }
        }
    }

    public void CountButtonPresses()
    {
        if (isVideoPlaying) // Solo contar los clics si el video se est� reproduciendo
        {
            clickCount++;
            Debug.Log("El bot�n ha sido clickeado " + clickCount + " veces.");
        }
    }

    private void OnVideoStart(VideoPlayer vp)
    {
        isVideoPlaying = true;
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        isVideoPlaying = false;

        if (isBeingGazed)
        {
            float gazeEndTime = Time.time; // Registrar el final del intervalo de mirada
            totalGazeTime += gazeEndTime - gazeStartTime; // Sumar al tiempo total de mirada
            gazeIntervals.Add((gazeStartTime, gazeEndTime)); // Agregar el intervalo a la lista
            isBeingGazed = false;
        }

        // Mostrar solo los intervalos de mirada
        string intervalsInfo = "Intervalos que se vio al video:\n";
        foreach (var interval in gazeIntervals)
        {
            float intervalDuration = interval.Item2 - interval.Item1;
            intervalsInfo += intervalDuration.ToString("F2") + " segundos\n";
        }

        // Calcular el tiempo total de mirada en el texto
        string totalTimeInfo = "Tiempo total de mirada al video: " + totalGazeTime.ToString("F2") + " segundos";

        // Mostrar el n�mero de veces que se presion� el bot�n durante la reproducci�n del video
        string clickInfo = "Se oy� la palabra 'estr�s' " + clickCount + " veces.";

        // Mostrar el tiempo total de mirada y los intervalos en el texto junto con el clickInfo
        gazeInfoText.text = intervalsInfo + "\n" + totalTimeInfo + "\n\n" + clickInfo;

        // Ocultar todas las figuras
        foreach (GameObject figure in allFigures)
        {
            figure.SetActive(false);
        }

        // Mostrar el canvas de resultados
        resultsCanvas.SetActive(true);
    }

    private void OnDestroy()
    {
        // Limpiar los listeners al destruir el objeto para evitar posibles errores
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
            videoPlayer.started -= OnVideoStart;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * maxGazeDistance);
    }
}
