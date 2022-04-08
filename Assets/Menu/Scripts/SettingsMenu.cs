using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Slider soundEffectsSlider;

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("Music");
        soundEffectsSlider.value = PlayerPrefs.GetFloat("SoundEffects");

    }
    
    public void MusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("Music", volume);
        PlayerPrefs.Save();
    }

    public void SetSoundEffectsVolume(float volume)
    {
        PlayerPrefs.SetFloat("SoundEffects", volume);
        PlayerPrefs.Save();
    }
}
