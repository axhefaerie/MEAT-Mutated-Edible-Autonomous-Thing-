using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Save previoues scene
    public static string previousScene; // name
    public static int previousSceneIndex = -1; // index

    // Save previous gameplay
    public static string lastGameplayScene;

    // LOAD SCENES ------------------------------------

    // Load with scene name:
    public static void LoadScene(string sceneName)
    {
        previousScene = SceneManager.GetActiveScene().name;
        //previousSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneName.StartsWith("Level"))
            lastGameplayScene = sceneName;

        SceneManager.LoadScene(sceneName);
    }

    // Load by index
    public static void LoadScene(int buildIndex)
    {
        previousSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(buildIndex);
    }

    // Load next scene
    public static void LoadNextScene()
    {
        previousScene = SceneManager.GetActiveScene().name;
        previousSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // GOING BACK -----------------------------------------
    // Going to de previous scene (w name)
    public static void LoadPreviousScene()
    {
        if (!string.IsNullOrEmpty(previousScene))
        {
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            Debug.LogWarning("No previous scene stored.");
        }
    }
    // RELOAD GAME ----------------------------------------
    // Back to LAST LEVEL PLAYED (Game over)
    public static void RestartLevel()
    {
        if (!string.IsNullOrEmpty(lastGameplayScene))
        {
            SceneManager.LoadScene(lastGameplayScene);
        }
        else
        {
            Debug.LogWarning("No gameplay scene stored");
        }
    }

    // RELOAD CURRENT
    public static void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // EXIT
    public static void QuitGame()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }


}

