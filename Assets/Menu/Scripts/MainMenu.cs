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
        // Check if any sound setting have been ever saved
        var soundEffectsVolume = PlayerPrefs.HasKey("FroggersSoundEffects");
        var musicVolume = PlayerPrefs.HasKey("FroggersMusic");
        if (!soundEffectsVolume)
        {
            PlayerPrefs.SetFloat("FroggersSoundEffects", 1);
        }

        if (!musicVolume)
        {
            PlayerPrefs.SetFloat("FroggersMusic", 1);
        }
        audioManager = FindObjectOfType<AudioManager>();
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
