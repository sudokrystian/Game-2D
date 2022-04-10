using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadingBoard : MonoBehaviour
{
    private GameController gameController;
    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        throw new NotImplementedException();
    }
}
