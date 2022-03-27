using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private float bonusHealth;
    void Start()
    {
        // Never allow empty fruits
        if (bonusHealth == 0)
        {
            bonusHealth = 1;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.GetComponent<Character2DController>();
        if (player)
        {
            player.AddHealth(bonusHealth);
            Destroy(gameObject);
        }
    }
}
