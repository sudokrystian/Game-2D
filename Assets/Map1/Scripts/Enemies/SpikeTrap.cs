using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private int spikeTrapDamage = 3;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Character2DController>();
        if (player)
        {
            player.TakeHit(spikeTrapDamage);
        }
        var enemy = collision.collider.GetComponent<EnemyStats>();
        if (enemy)
        {

            enemy.TakeHit(spikeTrapDamage);
        }
    }
}
