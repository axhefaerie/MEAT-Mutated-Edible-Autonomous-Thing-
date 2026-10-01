using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public void Back()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
