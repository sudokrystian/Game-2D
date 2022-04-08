using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFall : MonoBehaviour
{
    [SerializeField] private GameController GameController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Character2DController>();
        if (player)
        {
            GameController.GameOver();
        }
    }
}
