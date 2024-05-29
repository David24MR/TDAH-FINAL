using UnityEngine;
using UnityEngine.Video;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GazeWatchTracker gazeWatchTracker;
    public TextMeshProUGUI infoText;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Obtener los intervalos de tiempo vistos
        string intervalsInfo = "Intervalos vistos:\n";
        List<float> intervals = GetWatchIntervals();
        foreach (float interval in intervals)
        {
            intervalsInfo += interval.ToString("F2") + " segundos\n";
        }

        // Obtener el tiempo total visto sumando todos los intervalos
        float totalTime = intervals.Sum();

        // Crear el mensaje final con los intervalos y el tiempo total
        string finalMessage = intervalsInfo + "\nTiempo total visto: " + totalTime.ToString("F2") + " segundos";

        // Mostrar el mensaje en el panel de UI
        infoText.text = finalMessage;

        // Lógica adicional al finalizar el video
        Debug.Log("Video has ended.");
    }

    // Método para obtener los intervalos vistos
    List<float> GetWatchIntervals()
    {
        return gazeWatchTracker.GetWatchIntervals();
    }

    public void PlayVideo()
    {
        videoPlayer.Play();
    }

    public void PauseVideo()
    {
        videoPlayer.Pause();
    }
}