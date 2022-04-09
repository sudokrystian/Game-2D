using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpRecovery : MonoBehaviour
{
    [SerializeField] private int recoverHealth = 1;

    private void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.GetComponent<Character2DController>();
        if (player)
        {
            player.RecoverHealth(recoverHealth);
            Destroy(gameObject);
        }
    }
}
