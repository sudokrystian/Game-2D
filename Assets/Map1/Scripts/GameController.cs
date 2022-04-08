using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Audio manager
    [SerializeField] private AudioManager audioManager;
    // Camera behaviour
    [SerializeField] private Transform followObject;
    // Game over behaviour
    [SerializeField] private GameOverScreen GameOverScreen;
    // Pop up windows
    [SerializeField] private PopUpWindow popUpWindow;
    // Y offset of the camera
    [SerializeField] private float mainCameraYOffset = 4;
    
    private Camera MainCamera;

    private void Start()
    {
        MainCamera = Camera.main;   
    }


    private void FixedUpdate()
    {
        MainCamera.transform.position =
            new Vector3(followObject.position.x, followObject.position.y + mainCameraYOffset, this.transform.position.z);
    }
    
    public void GameOver()
    {
        GameOverScreen.Setup();
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
