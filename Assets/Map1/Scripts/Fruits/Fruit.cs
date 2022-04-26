using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Character2DController>();
        if (player)
        {
            Action(player);
            Destroy(gameObject);
        }
    }

    public virtual void Action(Character2DController player)
    {
        throw new NotImplementedException();
    }
}
