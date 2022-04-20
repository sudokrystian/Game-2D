using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioManager audioManager;
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        // Check if any sound setting have been ever saved
        bool soundEffectsVolume = audioManager.IsSoundEffectsVolumeSaved();
        var musicVolume = audioManager.IsMusicVolumeSaved();
        // If not set them to max
        if (!soundEffectsVolume)
        {
            audioManager.SetSoundEffectsVolume(1);
        }

        if (!musicVolume)
        {
            audioManager.SetMusicVolume(1);
        }
        audioManager.PlayMusic("MenuTheme");
        audioManager.UpdateMusicVolume();
        audioManager.UpdateSoundEffectsVolume();
    }

    public void PlayGame()
    {
        audioManager.PlaySoundEffect("ButtonClick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        audioManager.PlaySoundEffect("ButtonClick");
        Application.Quit();
    }
    
    public void PlayButtonSound()
    {
        audioManager.PlaySoundEffect("ButtonClick");
    }
}
