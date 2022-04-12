using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject mainMenuUI;
    public GameObject optionsMenuUI;
    
    // Audio
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        audioManager.PlaySoundEffect("Pause");
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        gameIsPaused = false;
    }

    private void Pause()
    {
        mainMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
        audioManager.PlaySoundEffect("Pause");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void BackToMainMenu()
    {
        PlayButtonSound();
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    
    public void QuitGame()
    {
        PlayButtonSound();
        Time.timeScale = 1f;
        Application.Quit();
    }
    
    public void PlayButtonSound()
    {
        audioManager.PlaySoundEffect("ButtonClick");
    }
}
