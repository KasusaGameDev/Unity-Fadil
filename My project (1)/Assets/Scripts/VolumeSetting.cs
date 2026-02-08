using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSFX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            setMusicVolume();
            setSFXVolume();
        }
    }
    public void setMusicVolume()
    {
        float volume = sliderMusic.value;
        audioMixer.SetFloat("music", math.log10(volume) * 40);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    public void setSFXVolume()
    {
        float volume = sliderSFX.value;
        audioMixer.SetFloat("sfx", math.log10(volume) * 40);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }
    private void LoadVolume()
    {
        sliderMusic.value = PlayerPrefs.GetFloat("musicVolume");
        sliderSFX.value = PlayerPrefs.GetFloat("sfxVolume");
        setMusicVolume();
        setSFXVolume();
    }
}
