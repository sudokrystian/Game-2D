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
            new Vector3(followObject.position.x, followObject.position.y + mainCameraYOffset, this.transform.position.z);
    }
    
    public void GameOver()
    {
        gameOverScreen.Setup();
    }

    public void DamageUpPopUp()
    {
        audioManager.Play("DmgUp");
        popUpWindow.ActivatePopUp("Damage Up!");
    }
    
    public void HealthRecoverPopUp() {
        audioManager.Play("HealthRecover");
        popUpWindow.ActivatePopUp("You feel more healthy");
    }
    
    public void HealthUpPopUp() {
        audioManager.Play("HealthUp");
        popUpWindow.ActivatePopUp("Max HP increased!");
    }
}
