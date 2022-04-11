using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public void PerformActionOnCollision(Collision2D collision, float actionValue)
    {
        var player = collision.collider.GetComponent<Character2DController>();
        if (player)
        {
            Action(player, actionValue);
            Destroy(gameObject);
        }
    }

    public virtual void Action(Character2DController player, float actionValue)
    {
        
    }
}
