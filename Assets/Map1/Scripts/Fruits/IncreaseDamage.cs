using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamage : MonoBehaviour
{
    [SerializeField] private int damageBonus = 1;
    private void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.GetComponent<Character2DController>();
        if (player)
        {
            player.IncreaseDamage(damageBonus);
            Destroy(gameObject);
        }
    }
}
