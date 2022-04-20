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
        musicSlider.value = audioManager.GetMusicVolume();
        soundEffectsSlider.value = audioManager.GetSoundEffectsVolume();
    }
    
    public void MusicVolume(float volume)
    {
        audioManager.PlaySoundEffect("AdjustSlider");
        audioManager.SetMusicVolume(volume);
        audioManager.UpdateMusicVolume();
    }

    public void SoundEffectsVolume(float volume)
    {
        audioManager.PlaySoundEffect("AdjustSlider");
        audioManager.SetSoundEffectsVolume(volume);
        audioManager.UpdateSoundEffectsVolume();
    }

    public void PlayButtonSound()
    {
        audioManager.PlaySoundEffect("ButtonClick");
    }
}
