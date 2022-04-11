using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpRecovery : Fruit
{
    [SerializeField] private int recoverHealth = 1;

    private void OnCollisionEnter2D(Collision2D col)
    {
        PerformActionOnCollision(col, recoverHealth);
    }

    public override void Action(Character2DController player, float actionValue)
    {
        player.RecoverHealth(recoverHealth);
    }
}
