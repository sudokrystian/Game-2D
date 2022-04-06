using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
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
        popUpWindow.ActivatePopUp("Damage Up!");
    }
}
