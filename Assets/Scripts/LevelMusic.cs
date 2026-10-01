using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelMusic : MonoBehaviour
{
    public AudioClip music;
    public AudioClip ambient;

    void Start()
    {
        if (AudioManager.Instance != null)
        {
            if (music != null)
                AudioManager.Instance.PlayMusic(music);

            if (ambient != null)
                AudioManager.Instance.PlayAmbient(ambient);

            // Bajar volumen de música a 70%
            //AudioManager.Instance.SetMusicVolume(0.7f);
        }
        
    }

}