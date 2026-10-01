using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSFX : MonoBehaviour
{
    [Header("Sound")]
    public AudioClip clickSound;
    public float volume = 1f;

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
        if (AudioManager.Instance != null && clickSound != null)
        {
            AudioManager.Instance.PlaySFX(clickSound, volume);
        }
    }

    void OnDestroy()
    {
        button.onClick.RemoveListener(PlaySound);
    }
    
}
