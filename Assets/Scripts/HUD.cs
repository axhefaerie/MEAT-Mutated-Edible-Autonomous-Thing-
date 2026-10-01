using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;


public class HUD : MonoBehaviour, IObserver
{
    public GameObject inicialText;
    public float duration = 7f; // time
    public float fadeTime = 1f;
/*
    public AudioClip ambientSource;
    public AudioClip musicSource;


    public Slider musicSlider;
    public Slider ambientSlider;


    public SettingsMenu settingsMenu;*/

    public Image[] lifes;

    [Header("Heart Sprites")]
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    public GameObject finalDialogue;

    public GameObject pausePanel;

    public string LevelName;

    [Header("CONTADOR DE CARNES")]
    public TMP_Text lifeCounterText;
    public int extraLifeCount = 0;

    [Header("Panel Fade In")]
    public CanvasGroup fadeInPanel;
    public float fadeDuration = 1f;
    public float visibleTime = 2f;

    [Header("Enemies Popup")]
    public TMP_Text enemiesPopupText;
    public float popupDuration = 3f;

    private Coroutine enemiesPopupCoroutine;

    void Start()
    {
        finalDialogue.SetActive(false);
        GameManager.Instance.AddObserver(this);



        // Ocultar menu pausa
        if (pausePanel != null)
            pausePanel.SetActive(false);

        // Ocultar letrero warning
        if (enemiesPopupText != null)
            enemiesPopupText.gameObject.SetActive(false);

        Time.timeScale = 1f;

        // Actualiza HUD con los valores actuales inmediatamente
        UpdateHUDImmediate();


        // FADE IN:
        if (fadeInPanel != null)
        {
            fadeInPanel.alpha = 1f;
            fadeInPanel.interactable = true;
            fadeInPanel.blocksRaycasts = true;

            StartCoroutine(FadeOutAfterDelay(fadeInPanel, fadeDuration, visibleTime));
        }

        LevelName = SceneManager.GetActiveScene().name;

        if (LevelName == "Level1")
        {
            StartCoroutine(MostrarTextoInicial());
        }

    }

    private System.Collections.IEnumerator FadeOutAfterDelay(CanvasGroup panel, float fadeTime, float stayTime)
    {
        // Esperar mientras el panel está visible
        yield return new WaitForSeconds(stayTime);

        // FADE OUT
        float elapsed = 0f;
        while (elapsed < fadeTime)
        {
            elapsed += Time.deltaTime;
            panel.alpha = Mathf.Clamp01(1 - (elapsed / fadeTime));
            yield return null;
        }

        panel.alpha = 0f;
        panel.interactable = false;
        panel.blocksRaycasts = false;
    }


    void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.RemoveObserver(this);
    }

    public void ShowEnemiesRemaining(int remaining)
    {
        if (enemiesPopupText == null) return;

        enemiesPopupText.text = $"You must collect all meat pieces and kill all enemies to continue!\r\nEnemies left: {remaining}";
        enemiesPopupText.gameObject.SetActive(true);

        if (enemiesPopupCoroutine != null)
            StopCoroutine(enemiesPopupCoroutine);

        enemiesPopupCoroutine = StartCoroutine(HideEnemiesPopup());
    }

    private IEnumerator HideEnemiesPopup()
    {
        yield return new WaitForSeconds(popupDuration);
        enemiesPopupText.gameObject.SetActive(false);
    }

    private System.Collections.IEnumerator MostrarTextoInicial()
    {
        Debug.Log("MOSTRANDO TEXTO");

        Image img = inicialText.GetComponent<Image>();
        if (img == null)
        {
            Debug.LogError("InicialText no tiene Image");
            yield break;
        }

        Color color = img.color;
        color.a = 0f;
        img.color = color;

        yield return new WaitForSeconds(2f);

        inicialText.SetActive(true);

        // -------- FADE IN --------
        float elapsed = 0f;
        while (elapsed < fadeTime)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / fadeTime);
            img.color = color;
            yield return null;
        }

        color.a = 1f;
        img.color = color;

        // -------- TIEMPO VISIBLE --------
        yield return new WaitForSeconds(duration);

        // -------- FADE OUT --------
        elapsed = 0f;
        while (elapsed < fadeTime)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(1 - (elapsed / fadeTime));
            img.color = color;
            yield return null;
        }

        color.a = 0f;
        img.color = color;

        inicialText.SetActive(false);

        Debug.Log("OCULTANDO TEXTO");
    }


    // ------------------- DIALOGUE -------------------
    public void ShowDialogue()
    {
        finalDialogue.SetActive(true);
    }

    public void HideDialogue()
    {
        finalDialogue.SetActive(false);
    }

    // ------------------- LIFES ------------------- 
    public void OnNotify(int currentLifes, int lifesCollected)
    {
        if (lifes == null || lifes.Length == 0) return;
        if (lifeCounterText == null) return;

        for (int i = 0; i < lifes.Length; i++)
        {
            if (lifes[i] == null) continue;

            int heartLife = currentLifes - (i * 2);

            if (heartLife >= 2)
                lifes[i].sprite = fullHeart;
            else if (heartLife == 1)
                lifes[i].sprite = halfHeart;
            else
                lifes[i].sprite = emptyHeart;
        }

        lifeCounterText.text = (lifesCollected + extraLifeCount).ToString();
    }


    //----------------------SONIDO/Sliders-----------------
   /* public void funcionamientoSliders()
    {
        if (musicSlider == null || ambientSlider == null) return;

        musicSlider.onValueChanged.RemoveAllListeners();
        ambientSlider.onValueChanged.RemoveAllListeners();

        musicSlider.value = AudioManager.Instance.GetMusicVolume();
        ambientSlider.value = AudioManager.Instance.GetAmbientVolume();

        musicSlider.onValueChanged.AddListener(OnMusicChanged);
        ambientSlider.onValueChanged.AddListener(OnAmbientChanged);
    }
    public void OnMusicChanged(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
    }

    public void OnAmbientChanged(float value)
    {
        AudioManager.Instance.SetAmbientVolume(value);
    }*/

    // ------------------- PAUSA -------------------
    public void Pause()
    {
        if (pausePanel != null)
            pausePanel.SetActive(true);  // mostramos el menú de pausa

        Time.timeScale = 0f; // pausamos todo lo que dependa de Time.deltaTime
       // funcionamientoSliders();

    }

    public void Resume()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false); // ocultamos el menú

        Time.timeScale = 1f;           // reanudamos el juego
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        GameManager.Instance.RestartLevel(LevelName);
    }


    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
       // AudioManager.Instance.StopAmbient(ambientSource);
        
    }


    private void UpdateHUDImmediate()
    {
        // Obtiene el estado actual del GameManager
        if (GameManager.Instance != null)
            OnNotify(GameManager.Instance.lifes, GameManager.Instance.totalLifesCollected);
    }
}
