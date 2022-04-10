using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamage : MonoBehaviour
{
    [SerializeField] private int damageBonus = 1;

    private void OnCollisionEnter2D(Collision2D col)
    {
        var player = col.collider.GetComponent<Character2DController>();
        if (player)
        {
            player.IncreaseDamage(damageBonus);
            Destroy(gameObject);
        }
    }
}
