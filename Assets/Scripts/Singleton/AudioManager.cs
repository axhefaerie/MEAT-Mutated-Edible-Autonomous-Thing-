using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource; // música
    public AudioSource sfxSource;   // efectos de sonido
    public AudioSource ambientSource; // ambient

    [Header("Fade Settings")]
    public float fadeDuration = 1.5f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Asegurarnos de que los AudioSource existan
            if (musicSource == null)
                musicSource = gameObject.AddComponent<AudioSource>();

            if (ambientSource == null)
                ambientSource = gameObject.AddComponent<AudioSource>();

            if (sfxSource == null)
                sfxSource = gameObject.AddComponent<AudioSource>();

            sfxSource.ignoreListenerPause = true;

        }
        else
        {
            Destroy(gameObject);
        }

        musicSource.volume = GetMusicVolume();
        sfxSource.volume = GetSFXVolume();
        ambientSource.volume = GetAmbientVolume();

    }

    // ---------------- MÚSICA ----------------
    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;

        if (musicSource == null)
        {
            Debug.LogWarning("Music AudioSource is missing!");
            return;
        }

        // Si ya está sonando esta música, no la reiniciamos
        if (musicSource.isPlaying && musicSource.clip == clip)
            return;

        // Lanza el fade
        StartCoroutine(FadeToNewMusic(musicSource, clip));

    }

    public void StopMusic()
    {
        if (musicSource == null) return;

        musicSource.Stop();
        musicSource.clip = null;
    }

    // ---------------- AMBIENT ----------------
    public void PlayAmbient(AudioClip clip)
    {
        if (clip == null) return;
        if (ambientSource == null)
        {
            ambientSource = gameObject.AddComponent<AudioSource>();
        }

        if (ambientSource.isPlaying && ambientSource.clip == clip)
            return;

        StartCoroutine(FadeToNewMusic(ambientSource, clip));
    }

    public void StopAmbient(AudioClip clip)
    {
        
            if (ambientSource == null) return;

            ambientSource.Stop();
            ambientSource.clip = null;
        
    }

    // ---------------- CORUTINE FOR FADE ----------------
    private IEnumerator FadeToNewMusic(AudioSource source, AudioClip newClip)
    {
        // Fade Out si estaba sonando
        if (source.isPlaying)
        {
            float startVolume = source.volume;
            float elapsed = 0f;

            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                source.volume = Mathf.Lerp(startVolume, 0f, elapsed / fadeDuration);
                yield return null;
            }

            source.Stop();
            source.volume = startVolume; // restauramos volumen
        }

        // Cambiar clip y reproducir
        source.clip = newClip;
        source.loop = true;
        source.Play();

        // Fade In
        source.volume = 0f;
        float fadeInElapsed = 0f;

        while (fadeInElapsed < fadeDuration)
        {
            fadeInElapsed += Time.deltaTime;
            source.volume = Mathf.Lerp(0f, 1f, fadeInElapsed / fadeDuration);
            yield return null;
        }

        source.volume = 1f;
    }

    // ---------------- SFX ----------------
    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;

        if (sfxSource == null)
        {
            Debug.LogWarning("SFX AudioSource is missing!");
            return;
        }

        sfxSource.PlayOneShot(clip, volume);
    }

    // ---------------- VOLUMEN ----------------
    public void SetMusicVolume(float value)
    {
        if (musicSource != null)
            musicSource.volume = value;

        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void SetSFXVolume(float value)
    {
        if (sfxSource != null)
            sfxSource.volume = value;

        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    public void SetAmbientVolume(float value)
    {

        if (ambientSource != null)
            ambientSource.volume = value;

        PlayerPrefs.SetFloat("AmbientVolume", value);
    }

    public float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat("MusicVolume", 1f);
    }

    public float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat("SFXVolume", 1f);
    }

    public float GetAmbientVolume()
    {
        return PlayerPrefs.GetFloat("AmbientVolume", 1f);
    }

}
