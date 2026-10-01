using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    public int levelNumber; // 1, 2 o 3
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();

        // Desactivamos el botón si el jugador no ha desbloqueado este nivel
        if (GameManager.Instance != null)
        {
            if (levelNumber > GameManager.Instance.lastLevelCompleted + 1)
                button.interactable = false; // bloqueado
        }
    }
}

