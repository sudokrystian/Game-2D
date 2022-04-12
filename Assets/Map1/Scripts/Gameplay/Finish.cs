using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public GameObject gameWonScreen;
    public TextMeshProUGUI gameWonText;
    private bool gameRunning = true;
    private float timer = 0;
    // Audio
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        if (gameRunning)
        {
            timer += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var player = collider.GetComponent<Character2DController>();
        if (player)
        {
            print("Finish!");
            WinActions();
        }
    }
    
    public void RestartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Map1");
    }
    
    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    private void WinActions()
    {
        audioManager.PlaySoundEffect("Win");
        audioManager.PlaySoundEffect("ItIsWednesday");
        gameRunning = false;
        gameWonText.text = "You win! \nThis run took you " + Math.Round(timer, 2) + " seconds";
        Time.timeScale = 0f;
        gameWonScreen.SetActive(true);
    }
}
