using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;

public class VideoController : MonoBehaviour, IPointerClickHandler
{
    public VideoPlayer videoPlayer; // Arrastra aqu� el componente VideoPlayer desde el inspector

    void Start()
    {
        // Aseg�rate de que el video no se reproduzca autom�ticamente al iniciar la escena
        if (videoPlayer != null)
        {
            videoPlayer.playOnAwake = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayVideo();
    }

    void PlayVideo()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Play();
        }
    }
}