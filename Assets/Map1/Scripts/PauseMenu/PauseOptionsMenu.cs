using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseOptionsMenu : MonoBehaviour
{
    public AudioManager audioManager;
    public Slider musicSlider;
    public Slider soundEffectsSlider;
    public Slider zoomOutSlider;

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("Music");
        soundEffectsSlider.value = PlayerPrefs.GetFloat("SoundEffects");
        zoomOutSlider.value = PlayerPrefs.GetFloat("CameraSize");
    }
    
    public void MusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("Music", volume);
        PlayerPrefs.Save();
        audioManager.UpdateVolume();
    }

    public void SetSoundEffectsVolume(float volume)
    {
        PlayerPrefs.SetFloat("SoundEffects", volume);
        PlayerPrefs.Save();
        audioManager.UpdateVolume();
    }

    public void CameraSize(float size)
    {
        PlayerPrefs.SetFloat("CameraSize", size);
        PlayerPrefs.Save();
        Camera.main.orthographicSize = size;
    }
}
