using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuMusic : MonoBehaviour
{

    public AudioClip menuMusic;

    void Start()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayMusic(menuMusic);
        }

    }

    void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene.StartsWith("Level"))
        {
            AudioManager.Instance.StopMusic();
        }
    }

}

