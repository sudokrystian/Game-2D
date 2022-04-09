using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Audio manager
    [SerializeField] private AudioManager audioManager;
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
    }

    private void Update()
    {
        // if (Input.GetButtonDown("Cancel"))
        // {
        //     // SceneManager.LoadScene(0);
        // }
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
        popUpWindow.ActivatePopUp("Damage up by " + damage + "!");
    }
    
    public void HealthRecoverPopUp(int health) {
        audioManager.Play("HealthRecover");
        popUpWindow.ActivatePopUp(health + " health recovered");
    }
    
    public void HealthUpPopUp(int health) {
        audioManager.Play("HealthUp");
        popUpWindow.ActivatePopUp("Max HP increased by " + health + "!");
    }

    public void ManaUp(int mana)
    {
        audioManager.Play("ManaUp");
        popUpWindow.ActivatePopUp("Max mana increased by " + mana + "!");
    }

    public void MovementUp(float movement)
    {
        audioManager.Play("SpeedUp");
        popUpWindow.ActivatePopUp("Movement speed increased by " + movement + "!");
    }
}
