using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUp : Fruit
{
    [SerializeField] private int extraHealth = 1;

    public override void Action(Character2DController player)
    {
        player.IncreaseHealth(extraHealth);
    }
}
