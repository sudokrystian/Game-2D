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
    private string playerPrefsCameraSizeKey = "FroggersCameraSize";
    
    // UI animations
    public Animator loadBulletAnimator;
    private readonly int loadBulletHash = Animator.StringToHash("LoadBullet");


    private void Start()
    {
        mainCamera = Camera.main;   
        audioManager = FindObjectOfType<AudioManager>();
        // Set up collisions rules
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Pick-ups"),LayerMask.NameToLayer("EnemyBullets"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Pick-ups"),LayerMask.NameToLayer("Bullets"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Pick-ups"),LayerMask.NameToLayer("Enemies"), true);
        // Enable bullets through platforms
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullets"),LayerMask.NameToLayer("Platforms"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Platforms"),LayerMask.NameToLayer("EnemyBullets"), true);
        
        // For now there is only one map so let's play the music
        PlayFirstMapTheme();
        // Adjust camera size
        if (IsCameraSizeSaved())
        {
            SetCameraSize(GetCameraSize());
        }
        else
        {
            // If not saved set it to the default value
            SetCameraSize(6.3f);
        }
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
        audioManager.PlaySoundEffect("DmgUp");
        popUpWindow.ActivatePopUpWithTimer("Damage up by " + damage + "!");
    }
    
    public void HealthRecoverPopUp(int health) {
        audioManager.PlaySoundEffect("Healing");
        popUpWindow.ActivatePopUpWithTimer(health + " health recovered");
    }

    public void HealthIsFullPopUp()
    {
        audioManager.PlaySoundEffect("Healing");
        popUpWindow.ActivatePopUpWithTimer("Health is full!");
    }
    
    public void MaxHealthUpPopUp(int health) {
        audioManager.PlaySoundEffect("HealthUp");
        popUpWindow.ActivatePopUpWithTimer("Max HP increased by " + health + "!");
    }

    public void ManaUp(int mana)
    {
        audioManager.PlaySoundEffect("ManaUp");
        popUpWindow.ActivatePopUpWithTimer("Max mana increased by " + mana + "!");
    }

    public void MovementUp(float movement)
    {
        audioManager.PlaySoundEffect("SpeedUp");
        popUpWindow.ActivatePopUpWithTimer("Movement speed increased by " + movement + "!");
    }
    
    public void MagicRangeUp(float magicRange)
    {
        audioManager.PlaySoundEffect("MagicRangeUp");
        popUpWindow.ActivatePopUpWithTimer("Magic range increased by " + magicRange + "!");
    }

    public void SetBulletAnimationLength(float bulletChargingTime)
    {
        float animationLength = 0.333f;
        
        loadBulletAnimator.speed = bulletChargingTime/animationLength;
    }
    public void PlayLoadBulletAnimation()
    {
        loadBulletAnimator.SetTrigger(loadBulletHash);
    }

    public void PlayFirstMapTheme()
    {
        audioManager.PlayMusic("Map1Forest");
    }

    public float GetCameraSize()
    {
        return PlayerPrefs.GetFloat(playerPrefsCameraSizeKey);
    }

    public void SetCameraSize(float size)
    {
        PlayerPrefs.SetFloat(playerPrefsCameraSizeKey, size);
        PlayerPrefs.Save();
        mainCamera.orthographicSize = size;
    }
    
    private bool IsCameraSizeSaved()
    {
        return PlayerPrefs.HasKey(playerPrefsCameraSizeKey);
    }

}
