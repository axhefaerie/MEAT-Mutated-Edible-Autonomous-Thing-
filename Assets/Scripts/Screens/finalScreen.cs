using UnityEngine;
using System.Collections;

public class FinalScreen : MonoBehaviour
{
    [Header("Fade Out Panel")]
    public CanvasGroup panel;
    public float fadeDuration = 1.5f;

    [Header("Audio")]
    public AudioClip sprinkle;
    public AudioClip distMusic;
    public float audioDelay = 1f;

    void Start()
    {
        GameManager.Instance.ResetLives();

        Time.timeScale = 1f; // por si vienes de pausa

        StopAllAudio();

        AudioManager.Instance.PlayMusic(distMusic);

        if (panel != null)
        {
            panel.alpha = 1f;              // empieza visible
            panel.blocksRaycasts = true;
            panel.interactable = true;

            StartCoroutine(FadeOut());
        }

        if (sprinkle != null)
            StartCoroutine(PlayAudioDelayed());
    }

    private IEnumerator FadeOut()
    {
        float elapsed = 0f;

        yield return new WaitForSeconds(2f);

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            panel.alpha = Mathf.Clamp01(1f - (elapsed / fadeDuration));
            yield return null;
        }

        panel.alpha = 0f;
        panel.blocksRaycasts = false;
        panel.interactable = false;
    }

    private IEnumerator PlayAudioDelayed()
    {
        yield return new WaitForSeconds(audioDelay);
        AudioManager.Instance.PlaySFX(sprinkle);
    }

    private void StopAllAudio()
    {
        if (AudioManager.Instance == null) return;

        //if (AudioManager.Instance.musicSource != null)
           // AudioManager.Instance.musicSource.Stop();

        if (AudioManager.Instance.ambientSource != null)
            AudioManager.Instance.ambientSource.Stop();

        if (AudioManager.Instance.sfxSource != null)
            AudioManager.Instance.sfxSource.Stop();
    }
}