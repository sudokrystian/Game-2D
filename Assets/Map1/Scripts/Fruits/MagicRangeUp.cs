using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicRangeUp : Fruit
{
    [SerializeField] private float magicRangeBonus = 1f;
    
    public override void Action(Character2DController player)
    {
        player.IncreaseMagicRange(magicRangeBonus);
    }
}
