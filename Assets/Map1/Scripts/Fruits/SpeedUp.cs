using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : Fruit
{
    [SerializeField] private float speedBonus = 0.5f;

    public override void Action(Character2DController player)
    {
        player.IncreaseSpeed(speedBonus);
    }
}
