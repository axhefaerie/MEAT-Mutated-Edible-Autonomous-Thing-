using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour
{
    public float fadeDuration = 1f;

    private Image fadeImage;

    void Awake()
    {
        fadeImage = GetComponent<Image>();
    }

    void Start()
    {
        StartCoroutine(FadeInRoutine());
    }

    private IEnumerator FadeInRoutine()
    {
        
        Color color = fadeImage.color;
        color.a = 0f;                // invisible
        fadeImage.color = color;

        float time = 0f;
        yield return new WaitForSeconds(6f);

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, time / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1f;                // visible
        fadeImage.color = color;
    }
}
