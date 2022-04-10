using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpRecovery : MonoBehaviour
{
    [SerializeField] private int recoverHealth = 1;

    private void OnCollisionEnter2D(Collision2D col)
    {
        var player = col.collider.GetComponent<Character2DController>();
        if (player)
        {
            player.RecoverHealth(recoverHealth);
            Destroy(gameObject);
        }
    }
}
