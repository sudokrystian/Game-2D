using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUp : Fruit
{
    [SerializeField] private int extraHealth = 1;

    private void OnCollisionEnter2D(Collision2D col)
    {
        PerformActionOnCollision(col, extraHealth);
    }

    public override void Action(Character2DController player, float actionValue)
    {
        player.IncreaseHealth(extraHealth);
    }
}
