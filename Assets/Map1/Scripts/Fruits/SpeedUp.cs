using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    [SerializeField] private float speedBonus = 0.5f;

    private void OnCollisionEnter2D(Collision2D col)
    {
        var player = col.collider.GetComponent<Character2DController>();
        if (player)
        {
            player.IncreaseSpeed(speedBonus);
            Destroy(gameObject);
        }
    }
}
