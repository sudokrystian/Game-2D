using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamage : Fruit
{
    [SerializeField] private int damageBonus = 1;

    private void OnCollisionEnter2D(Collision2D col)
    {
        PerformActionOnCollision(col, damageBonus);
    }

    public override void Action(Character2DController player, float actionValue)
    {
        player.IncreaseDamage(damageBonus);
    }
}
