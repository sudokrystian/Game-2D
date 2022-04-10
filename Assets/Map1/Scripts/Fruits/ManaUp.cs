using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaUp : MonoBehaviour
{
    [SerializeField] private int extraMana = 1;

    private void OnCollisionEnter2D(Collision2D col)
    {
        var player = col.collider.GetComponent<Character2DController>();
        if (player)
        {
            player.IncreaseMana(extraMana);
            Destroy(gameObject);
        }
    }
}
