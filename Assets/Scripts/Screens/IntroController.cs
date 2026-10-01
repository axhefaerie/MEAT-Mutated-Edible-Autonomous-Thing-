using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroController : MonoBehaviour
{
    public Image logo;
    public float fadeInTime = 1.5f;
    public float stayTime = 1.5f;
    public float fadeOutTime = 1.5f;
    public float startBlackTime = 1.5f; // tiempo inicial en negro
    public float endBlackTime = 1.5f;   // tiempo final en negro

    public string nextScene = "MainMenu";

    public AudioClip introSound;
    //public AudioClip music;

    void Start()
    {
        //AudioManager.Instance.PlayMusic(music);
        StartCoroutine(IntroSequence());
    }

    IEnumerator IntroSequence()
    {
        // NEGRO INICIAL
        yield return new WaitForSeconds(startBlackTime);
        AudioManager.Instance.PlaySFX(introSound);

        // Fade In
        yield return StartCoroutine(Fade(0f, 1f, fadeInTime));        

        // Esperar
        yield return new WaitForSeconds(stayTime);

        // Fade Out
        yield return StartCoroutine(Fade(1f, 0f, fadeOutTime));

        // NEGRO FINAL
        yield return new WaitForSeconds(endBlackTime);

        // Cargar siguiente escena
        SceneLoader.LoadScene(nextScene);
    }

    IEnumerator Fade(float from, float to, float duration)
    {
        float elapsed = 0f;
        Color color = logo.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(from, to, elapsed / duration);
            logo.color = color;
            yield return null;
        }

        color.a = to;
        logo.color = color;
    }
}
