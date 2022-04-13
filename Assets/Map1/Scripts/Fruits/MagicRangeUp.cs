using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicRangeUp : Fruit
{
    [SerializeField] private float magicRangeBonus = 1f;

    private void OnCollisionEnter2D(Collision2D col)
    {
        PerformActionOnCollision(col, magicRangeBonus);
    }

    public override void Action(Character2DController player, float actionValue)
    {
        player.IncreaseMagicRange(magicRangeBonus);
    }
}
