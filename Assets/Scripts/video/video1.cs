using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class video1 : MonoBehaviour
{
    [Header("Fade Settings")]
    public Image fadeImage;
    public float fadeInTime = 1.5f;
    public float startBlackTime = 1f; // tiempo inicial en negro
    public float endBlackTime = 1f;   // tiempo final en negro

    [Header("Sound Settings")]
    public AudioClip music;
    public AudioClip ambient;

    [Header("Timing")]
    public float timeToWait = 5f; // tiempo antes de cambiar de escena

    private bool hasStarted = false; // evita duplicar Invoke

    void Start()
    {
        if (hasStarted) return;
        hasStarted = true;

        // Validar fadeImage
        if (fadeImage == null)
        {
            Debug.LogWarning("No Fade Image assigned! Please assign one in the Inspector.");
        }

        // Iniciar secuencia
        StartCoroutine(IntroSequence());
    }

    IEnumerator IntroSequence()
    {
        // Tiempo inicial en negro
        if (startBlackTime > 0f)
            yield return new WaitForSeconds(startBlackTime);

        // Reproducir música y ambiente con fade
        if (music != null)
            AudioManager.Instance.PlayMusic(music);

        if (ambient != null)
            AudioManager.Instance.PlayAmbient(ambient);

        // Fade in
        if (fadeImage != null)
            yield return StartCoroutine(Fade(1f, 0f, fadeInTime));

        // Esperar antes de fade out
        yield return new WaitForSeconds(timeToWait);

        // Fade out
        if (fadeImage != null)
            yield return StartCoroutine(Fade(0f, 1f, fadeInTime));

        // Tiempo final en negro
        if (endBlackTime > 0f)
            yield return new WaitForSeconds(endBlackTime);

        // Cargar siguiente escena de forma segura
        ChangeScene();
    }

    IEnumerator Fade(float from, float to, float duration)
    {
        if (fadeImage == null) yield break;

        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(from, to, elapsed / duration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = to;
        fadeImage.color = color;
    }

    void ChangeScene()
    {
        //AudioManager.Instance.StopMusic();
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning("No hay más escenas en Build Settings");
            return;
        }

        SceneLoader.LoadScene(nextIndex);
    }

}
