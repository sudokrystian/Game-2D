using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpRecovery : Fruit
{
    [SerializeField] private int recoverHealth = 1;

    public override void Action(Character2DController player)
    {
        player.RecoverHealth(recoverHealth);
    }
}
