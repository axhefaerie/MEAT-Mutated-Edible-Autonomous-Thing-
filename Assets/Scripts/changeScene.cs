using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class changeScene : MonoBehaviour
{
    public string nextScene;

    public CanvasGroup fadePanel;      // Panel negro que cubrirá la pantalla
    public float fadeDuration = 1f;    // Duración del fade out

    private bool isChanging = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isChanging)
        {
            isChanging = true;
            StartCoroutine(FadeAndChangeScene());
        }
    }

    private IEnumerator FadeAndChangeScene()
    {
        if (fadePanel != null)
        {
            fadePanel.gameObject.SetActive(true);

            float elapsed = 0f;
            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                fadePanel.alpha = Mathf.Clamp01(elapsed / fadeDuration);
                yield return null;
            }

            fadePanel.alpha = 1f;
        }

        SceneManager.LoadScene(nextScene);
    }
}
