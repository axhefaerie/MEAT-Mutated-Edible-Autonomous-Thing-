using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class VideoEndScene : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string sceneName;

    [Header("Sound Settings")]
    public AudioClip soundToPlay;  
    public float delayBeforeSound = 0.5f; 

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished; // Cuando el video termine
        
        if (soundToPlay != null)
        {
            StartCoroutine(PlaySoundAfterDelay());
        }
    }

    IEnumerator PlaySoundAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeSound);

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(soundToPlay);
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        SceneLoader.LoadScene(sceneName); // Cambia de escena
    }
}

