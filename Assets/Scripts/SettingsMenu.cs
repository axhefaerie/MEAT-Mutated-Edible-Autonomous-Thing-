using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        // Inicializar sliders con valores actuales
        musicSlider.value = AudioManager.Instance.GetMusicVolume();
        sfxSlider.value = AudioManager.Instance.GetSFXVolume();

        // Escuchar cambios
        musicSlider.onValueChanged.AddListener(OnMusicChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXChanged);
    }

    public void OnMusicChanged(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
    }

    public void OnSFXChanged(float value)
    {
        AudioManager.Instance.SetSFXVolume(value);
        AudioManager.Instance.SetAmbientVolume(value);
    }
}
