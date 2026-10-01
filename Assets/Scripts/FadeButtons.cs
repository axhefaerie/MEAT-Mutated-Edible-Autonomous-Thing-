using UnityEngine;
using System.Collections;

public class FadeButtons : MonoBehaviour
{
    public CanvasGroup[] buttons;
    public float fadeDuration = 0.8f;

    void Start()
    {
        int prevIndex = SceneLoader.previousSceneIndex; // Asegúrate de actualizarlo en SceneLoader

        if (SceneLoader.previousScene == "Intro2")
            {

            // Venimos de la escena correcta: fade
            SetButtonsInvisible();
            StartCoroutine(FadeInAll());

        } else {

            // No venimos de la escena indicada: botones normales
            SetButtonsVisibleInstant();
        }
            
    }

    void SetButtonsInvisible()
    {
        foreach (CanvasGroup cg in buttons)
        {
            cg.alpha = 0f;
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }
    }

    void SetButtonsVisibleInstant()
    {
        foreach (CanvasGroup cg in buttons)
        {
            cg.alpha = 1f;
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }
    }

    IEnumerator FadeInAll()
    {
        yield return new WaitForSeconds(3f);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = elapsed / fadeDuration;

            foreach (CanvasGroup cg in buttons)
                cg.alpha = alpha;

            yield return null;
        }

        SetButtonsVisibleInstant();
    }
}

