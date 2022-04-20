using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseOptionsMenu : MonoBehaviour
{
    private GameController gameController;
    private AudioManager audioManager;
    public Slider musicSlider;
    public Slider soundEffectsSlider;
    public Slider zoomOutSlider;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        audioManager = FindObjectOfType<AudioManager>();
        musicSlider.value = audioManager.GetMusicVolume();
        soundEffectsSlider.value = audioManager.GetSoundEffectsVolume();
        zoomOutSlider.value = gameController.GetCameraSize();
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

    public void CameraSize(float size)
    {
        audioManager.PlaySoundEffect("AdjustSlider");
        gameController.SetCameraSize(size);
    }
}
