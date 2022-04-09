using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaUp : MonoBehaviour
{
    [SerializeField] private int extraMana = 1;

    private void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.GetComponent<Character2DController>();
        if (player)
        {
            player.IncreaseMana(extraMana);
            Destroy(gameObject);
        }
    }
}
