using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : Fruit
{
    [SerializeField] private float speedBonus = 0.5f;

    private void OnCollisionEnter2D(Collision2D col)
    {
        PerformActionOnCollision(col, speedBonus);
    }

    public override void Action(Character2DController player, float actionValue)
    {
        player.IncreaseSpeed(speedBonus);
    }
}
