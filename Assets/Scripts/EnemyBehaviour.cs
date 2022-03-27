using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    // Enemy components
    private Rigidbody2D rigidBody;
    [SerializeField] private HealthBar HealthBar;
    // Enemy stats
    [SerializeField] private float EnemySpeed = 1f;
    [SerializeField] private float EnemyMaxHealth = 5;
    [SerializeField] private float EnemyDamage = 1;
    
    private float Hitpoints;
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        Hitpoints = EnemyMaxHealth;
        HealthBar.SetMaxHealth(EnemyMaxHealth);
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        var player = col.collider.GetComponent<Character2DController>();
        if (player)
        {
            player.TakeHit(EnemyDamage);
        }

   
        print("kurwa: " + rigidBody.velocity.x);

        // transform.position += new Vector3(-horizontalMove, 0, 0) * Time.deltaTime * 10;
    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        var player = collisionInfo.collider.GetComponent<Character2DController>();
        if (player)
        {
            player.TakeHit(EnemyDamage);
        }
    }

    public void TakeHit(float damage)
    {
        Hitpoints -= damage;
        HealthBar.SetHealth(Hitpoints);
        if (Hitpoints <= 0)
        {
            Destroy(gameObject);
        }
    }

}
