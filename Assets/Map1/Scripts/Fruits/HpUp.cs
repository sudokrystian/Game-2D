using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUp : MonoBehaviour
{
    [SerializeField] private int extraHealth = 1;

    private void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.GetComponent<Character2DController>();
        if (player)
        {
            player.IncreaseHealth(extraHealth);
            Destroy(gameObject);
        }
    }
}
