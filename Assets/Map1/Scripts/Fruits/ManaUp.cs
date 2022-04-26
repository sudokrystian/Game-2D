using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaUp : Fruit
{
    [SerializeField] private int extraMana = 1;
    
    public override void Action(Character2DController player)
    {
        player.IncreaseMana(extraMana);
    }
}
