using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUp : MonoBehaviour
{
    [SerializeField] private int extraHealth = 1;

    private void OnCollisionEnter2D(Collision2D col)
    {
        var player = col.collider.GetComponent<Character2DController>();
        if (player)
        {
            player.IncreaseHealth(extraHealth);
            Destroy(gameObject);
        }
    }
}
