using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : Subject
{
    public static GameManager Instance;

    public int lastLevelCompleted = 0; // 0 = ninguno, 1 = nivel 1 completado, etc.
    public string currentLevelName;

    [Header("Sound effects")]
    public AudioClip deathSound;
    public AudioClip clickSound;

    [Header("Parameters")]
    public int lifes = 6;
    public const int MAX_LIFES = 6;

    public int totalLifesCollected = 0;
    public int levelStartLifesCollected = 0;

    [Header("Enemies")]
    public int totalEnemiesInLevel;
    public int enemiesRemaining;

    private Coroutine flashCoroutine;
    private Color playerOriginalColor;
    private bool originalColorSaved = false;

    void Awake()
    {
        //PlayerPrefs.DeleteAll(); // SOLO PARA PRUEBAS

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            lastLevelCompleted = PlayerPrefs.GetInt("LastLevelCompleted", 0);
            
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
            Destroy(gameObject);

    }

    public void LevelCompleted(int levelNumber)
    {
        if (levelNumber > lastLevelCompleted)
        {
            lastLevelCompleted = levelNumber;
            PlayerPrefs.SetInt("LastLevelCompleted", lastLevelCompleted);
            PlayerPrefs.Save();
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Se llama al entrar en cualquier escena
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
            Time.timeScale = 1f;

            if (scene.name.StartsWith("Level"))
            {
                currentLevelName = scene.name;

                // Contamos enemigos del nivel
                enemiesRemaining = GameObject.FindGameObjectsWithTag("Enemy").Length;
                totalEnemiesInLevel = enemiesRemaining;
            }

            if (scene.name.StartsWith("Level"))
            {
                totalLifesCollected = 0;   
                levelStartLifesCollected = 0;
            }
            NotifyObservers(lifes, totalLifesCollected);

    }

    public void EnemyKilled()
    {
        enemiesRemaining = Mathf.Max(0, enemiesRemaining - 1);
    }

    // ---------------- LIFES ----------------
    public void LooseLife(int amount)
    {
        lifes -= amount;
        NotifyObservers(lifes, totalLifesCollected);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                if (!originalColorSaved)
                {
                    playerOriginalColor = sr.color;
                    originalColorSaved = true;
                }

                if (flashCoroutine != null)
                    StopCoroutine(flashCoroutine);

                flashCoroutine = StartCoroutine(FlashPlayerRed(sr, 0.2f));
            }
        }

        if (lifes <= 0)
        {
            ResetLives();
            AudioManager.Instance.PlaySFX(deathSound);
            SceneManager.LoadScene("Morch");
        }
    }


    public void GainLife()
    {
        totalLifesCollected++;

        if (lifes < MAX_LIFES)
        {
            lifes++;

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    if (!originalColorSaved)
                    {
                        playerOriginalColor = sr.color;
                        originalColorSaved = true;
                    }

                    if (flashCoroutine != null)
                        StopCoroutine(flashCoroutine);

                    flashCoroutine = StartCoroutine(FlashPlayerGreen(sr, 0.2f));
                }
            }
        }

        NotifyObservers(lifes, totalLifesCollected);
    }

    private IEnumerator FlashPlayerRed(SpriteRenderer sr, float duration)
    {
        sr.color = Color.red;

        yield return new WaitForSeconds(duration);

        if (sr != null)
            sr.color = playerOriginalColor;
    }

    private IEnumerator FlashPlayerGreen(SpriteRenderer sr, float duration)
    {
        sr.color = Color.green;

        yield return new WaitForSeconds(duration);

        if (sr != null)
            sr.color = playerOriginalColor;
    }


    //Para resetear vidas al reiniciar
    public void ResetLives()
    {
        lifes = MAX_LIFES;
    }


    // ---------------- LEVEL CONTROL ----------------CONTADOR VIDAS
    public void RestartCurrentLevel()
    {
        ResetLives();
        totalLifesCollected = levelStartLifesCollected;
        NotifyObservers(lifes, totalLifesCollected);

        SceneManager.LoadScene(currentLevelName);
    }

    public void RestartLevel(string levelName)
    {
        totalLifesCollected = levelStartLifesCollected;
        NotifyObservers(lifes, totalLifesCollected);
        SceneManager.LoadScene(levelName);
    }

}