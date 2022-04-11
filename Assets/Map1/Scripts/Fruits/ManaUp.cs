using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaUp : Fruit
{
    [SerializeField] private int extraMana = 1;

    private void OnCollisionEnter2D(Collision2D col)
    {
        PerformActionOnCollision(col, extraMana);
    }

    public override void Action(Character2DController player, float actionValue)
    {
        player.IncreaseMana(extraMana);
    }
}
