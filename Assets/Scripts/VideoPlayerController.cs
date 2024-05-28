using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Asigna el componente VideoPlayer en el inspector

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // L�gica al finalizar el video (puedes a�adir alguna acci�n si es necesario)
        Debug.Log("Video has ended.");
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