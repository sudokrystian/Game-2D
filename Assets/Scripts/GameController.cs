using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Camera behaviour
    [SerializeField] private Transform followObject;
    // Game over behaviour
    [SerializeField] private GameOverScreen GameOverScreen;


    private void FixedUpdate()
    {
        this.transform.position =
            new Vector3(followObject.position.x, followObject.position.y + 4, this.transform.position.z);
    }
    
    public void GameOver()
    {
        GameOverScreen.Setup();
    }
}
