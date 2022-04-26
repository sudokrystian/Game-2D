using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamage : Fruit
{
    [SerializeField] private int damageBonus = 1;
    
    public override void Action(Character2DController player)
    {
        player.IncreaseDamage(damageBonus);
    }
}
