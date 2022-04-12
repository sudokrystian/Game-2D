using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Slider soundEffectsSlider;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        musicSlider.value = PlayerPrefs.GetFloat("FroggersMusic");
        soundEffectsSlider.value = PlayerPrefs.GetFloat("FroggersSoundEffects");
    }
    
    public void MusicVolume(float volume)
    {
        print("Adjust music volume to " + volume);
        audioManager.PlaySoundEffect("AdjustSlider");
        PlayerPrefs.SetFloat("FroggersMusic", volume);
        PlayerPrefs.Save();
        audioManager.UpdateMusicVolume();
    }

    public void SoundEffectsVolume(float volume)
    {
        audioManager.PlaySoundEffect("AdjustSlider");
        PlayerPrefs.SetFloat("FroggersSoundEffects", volume);
        PlayerPrefs.Save();
        audioManager.UpdateSoundEffectsVolume();
    }

    public void PlayButtonSound()
    {
        audioManager.PlaySoundEffect("ButtonClick");
    }
}
