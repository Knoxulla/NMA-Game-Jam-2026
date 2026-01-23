using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] AudioMixer myMixer;

    [Header("Sliders")]
    [SerializeField] Slider masterVolSlider;
    [SerializeField] Slider SFXVolSlider;
    [SerializeField] Slider musicVolSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("masterVol"))
        {
            LoadMasterVolume();
        }
        else
        {
            SetMasterVol();
        }

        if (PlayerPrefs.HasKey("musicVol"))
        {
            LoadMusicVolume();
        }
        else 
        { 
            SetMusicVol();
        }
    }


    public void SetMasterVol()
    { 
        float volume = masterVolSlider.value;
        myMixer.SetFloat("Master", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("masterVol", volume);
    }

    private void LoadMasterVolume()
    { 
        masterVolSlider.value = PlayerPrefs.GetFloat("masterVol");

        SetMasterVol();
    }

    public void SetMusicVol()
    {
        float volume = musicVolSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVol", volume);
    }

    private void LoadMusicVolume()
    {
        musicVolSlider.value = PlayerPrefs.GetFloat("musicVol");

        SetMusicVol();
    }

    public void SetSFXVol()
    {
        float volume = SFXVolSlider.value;
        myMixer.SetFloat("Sound Effects", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVol", volume);
    }

    private void LoadSFXVolume()
    {
        SFXVolSlider.value = PlayerPrefs.GetFloat("sfxVol");

        SetSFXVol();
    }

}
