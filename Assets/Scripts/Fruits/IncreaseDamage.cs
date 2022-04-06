using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.GetComponent<Character2DController>();
        if (player)
        {
            player.IncreaseDamage();
            Destroy(gameObject);
        }
    }
}
