using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    // Enemy components
    private Rigidbody2D rigidBody;
    public GameObject bloodPrefab;
    [SerializeField] private HealthBar HealthBar;
    // Enemy stats
    [SerializeField] private float EnemySpeed = 1f;
    [SerializeField] private float EnemyMaxHealth = 5;
    [SerializeField] private float EnemyDamage = 1;
    
    // Collision info
    private float timeColliding = 0;
    // Time before damage is taken
    private float timeThreshold = 0.5f;
    
    private float Hitpoints;
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        Hitpoints = EnemyMaxHealth;
        HealthBar.SetMaxHealth(EnemyMaxHealth);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Character2DController>();
        if (player)
        {
            player.TakeHit(EnemyDamage);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Deal damage every 0.5sec when touching
        var player = collision.collider.GetComponent<Character2DController>();
        if (player)
        {
            if (timeColliding < timeThreshold)
            {
                timeColliding += Time.deltaTime;
            }
            else
            {
                player.TakeHit(EnemyDamage);
                timeColliding = 0f;
            }
        }
    }
    

    public void TakeHit(float damage)
    {
        Instantiate(bloodPrefab, gameObject.transform.position, gameObject.transform.rotation);
        FindObjectOfType<AudioManager>().Play("BulletHit");
        Hitpoints -= damage;
        HealthBar.SetHealth(Hitpoints);
        if (Hitpoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void CollideWithPlayer(Collision2D collisionInfo)
    {
        var player = collisionInfo.collider.GetComponent<Character2DController>();
        if (player)
        {
            player.TakeHit(EnemyDamage);
        }
    }

}
