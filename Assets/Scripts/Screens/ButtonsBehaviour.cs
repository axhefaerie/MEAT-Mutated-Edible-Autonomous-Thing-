using UnityEngine;


public class ButtonsBehaviour : MonoBehaviour
{
    // PARA EL SONIDO DEL CLICK

  
// PLAY

public void Play()
    {

        SceneLoader.LoadScene("ChooseLvl");
    }

    // MAIN MENU
    public void MainMenu()
    {

        SceneLoader.LoadScene("MainMenu");
    }

    // LVL 1
    public void StartLvl1()
    {

        SceneLoader.LoadScene("Level1");
    }

    // LVL 2
    public void StartLvl2()
    {

        SceneLoader.LoadScene("Level2");
    }

    // LVL 3
    public void StartLvl3()
    {

        SceneLoader.LoadScene("Level3");
    }

    // SETTINGS
    public void Settings()
    {

        SceneLoader.LoadScene("Settings");
    }

    // ACHIEVEMENTS
    public void Achievements()
    {

        SceneLoader.LoadScene("Achievements");
    }

    // CREDITS
    public void Help()
    {

        SceneLoader.LoadScene("Help");
    }

    // HELP
    public void Credits()
    {

        SceneLoader.LoadScene("Credits");
    }

    // BACK
    public void Back()
    {

        SceneLoader.LoadPreviousScene();
    }

    // RESTART
    public void Restart()
    {

        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartCurrentLevel();
        }
    }


    // RESTART
    public void Reload()
    {

        SceneLoader.ReloadCurrentScene();
    }

    // EXIT
    public void Exit()
    {

        SceneLoader.QuitGame();
    }

}
