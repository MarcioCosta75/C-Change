using UnityEngine;
using UnityEngine.Video;

public class VideoEndSwitcher : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Refer�ncia para o VideoPlayer
    public GameObject objectToDeactivate; // Objeto para desativar ap�s o v�deo
    public GameObject objectToActivate; // Objeto para ativar ap�s o v�deo

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>(); // Tenta pegar o VideoPlayer do mesmo GameObject
        }

        videoPlayer.loopPointReached += EndReached; // Assina o evento
    }

    void EndReached(VideoPlayer vp)
    {
        objectToDeactivate.SetActive(false); // Desativa o objeto
        objectToActivate.SetActive(true); // Ativa o outro objeto
    }
}