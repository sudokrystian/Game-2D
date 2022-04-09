using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    [SerializeField] private float speedBonus = 0.5f;

    private void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.GetComponent<Character2DController>();
        if (player)
        {
            player.IncreaseSpeed(speedBonus);
            Destroy(gameObject);
        }
    }
}
