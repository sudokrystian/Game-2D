using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseOptionsMenu : MonoBehaviour
{
    private AudioManager audioManager;
    public Slider musicSlider;
    public Slider soundEffectsSlider;
    public Slider zoomOutSlider;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        musicSlider.value = PlayerPrefs.GetFloat("FroggersMusic");
        soundEffectsSlider.value = PlayerPrefs.GetFloat("FroggersSoundEffects");
        zoomOutSlider.value = PlayerPrefs.GetFloat("CameraSize");
    }
    
    public void MusicVolume(float volume)
    {
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

    public void CameraSize(float size)
    {
        audioManager.PlaySoundEffect("AdjustSlider");
        PlayerPrefs.SetFloat("CameraSize", size);
        PlayerPrefs.Save();
        Camera.main.orthographicSize = size;
    }
}
