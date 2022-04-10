using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Audio manager
     private AudioManager audioManager;
    // Camera behaviour
    [SerializeField] private Transform followObject;
    // Game over behaviour
    [SerializeField] private GameOverScreen gameOverScreen;
    // Pop up windows
    [SerializeField] private PopUpWindow popUpWindow;
    // Y offset of the camera
    [SerializeField] private float mainCameraYOffset = 1.3f;
    
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;   
        audioManager = FindObjectOfType<AudioManager>();
    }
    
    private void FixedUpdate()
    {
        mainCamera.transform.position =
            new Vector3(followObject.position.x, followObject.position.y + mainCameraYOffset, -1);
    }

    public void GameOver()
    {
        gameOverScreen.Setup();
    }
    public void DamageUpPopUp(int damage)
    {
        audioManager.Play("DmgUp");
        popUpWindow.ActivatePopUpWithTimer("Damage up by " + damage + "!");
    }
    
    public void HealthRecoverPopUp(int health) {
        audioManager.Play("HealthRecover");
        popUpWindow.ActivatePopUpWithTimer(health + " health recovered");
    }
    
    public void HealthUpPopUp(int health) {
        audioManager.Play("HealthUp");
        popUpWindow.ActivatePopUpWithTimer("Max HP increased by " + health + "!");
    }

    public void ManaUp(int mana)
    {
        audioManager.Play("ManaUp");
        popUpWindow.ActivatePopUpWithTimer("Max mana increased by " + mana + "!");
    }

    public void MovementUp(float movement)
    {
        audioManager.Play("SpeedUp");
        popUpWindow.ActivatePopUpWithTimer("Movement speed increased by " + movement + "!");
    }

}
