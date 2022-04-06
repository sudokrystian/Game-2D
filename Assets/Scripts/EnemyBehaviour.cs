using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    // Enemy components
    private Rigidbody2D rigidBody;
    public Transform enemyGFX;
    public GameObject bloodPrefab;
    [SerializeField] private HealthBar HealthBar;
    // Enemy stats
    [SerializeField] private float EnemySpeed = 250f;
    [SerializeField] private float EnemyMaxHealth = 5;
    [SerializeField] private float EnemyDamage = 1;
    // How far before the enemy can see the player
    [SerializeField] private float VisionRange = 8;
    private float Hitpoints;
    // Collision info
    private float timeColliding = 0;
    // Time before damage is taken
    private float timeThreshold = 0.5f;
    // Target for AI
    public Transform target;
    public float nextWaypointDistance = 1f;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    private Seeker seeker;
    
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        Hitpoints = EnemyMaxHealth;
        HealthBar.SetMaxHealth(EnemyMaxHealth);
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    private void FixedUpdate()
    {
        MoveEnemyTowardsTarget();
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

    private void MoveEnemyTowardsTarget()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        // Get the direction and force to move
        Vector2 direction = ((Vector2) path.vectorPath[currentWaypoint] - (Vector2) rigidBody.position).normalized;
        Vector2 force = direction * EnemySpeed * Time.deltaTime;
        // Move the enemy if he is in range
        float distanceFromPlayer = Vector2.Distance(rigidBody.position, target.position);
        if (distanceFromPlayer < VisionRange)
        {
            rigidBody.AddForce(force);
        }
        
        float distance = Vector2.Distance(rigidBody.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        // Rotate the enemy accordingly
        // if (force.x >= 0.01f)
        // {
        //     enemyGFX.localScale = new Vector3(-enemyGFX.localScale.x, enemyGFX.localScale.y, enemyGFX.localScale.z);
        // } else if (force.x <= -0.01f)
        // {
        //     enemyGFX.localScale = new Vector3(enemyGFX.localScale.x, enemyGFX.localScale.y, enemyGFX.localScale.z);
        // }
        if (force.x >= 0.01f)
        {
            enemyGFX.transform.rotation = Quaternion.Euler(0, 180, 0);

        } else if (force.x <= -0.01f)
        {
            enemyGFX.transform.rotation = Quaternion.identity;
        }
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rigidBody.position, target.position, OnPathComplete);
        }
    }
    
    private void OnPathComplete(Path path)
    {
        if (!path.error)
        {
            this.path = path;
            currentWaypoint = 0;
        }
    }
    

    public void TakeHit(float damage)
    {
        var blood = Instantiate(bloodPrefab, rigidBody.transform.position, rigidBody.transform.rotation);
        blood.GetComponent<ParticleSystem>().Play();
        FindObjectOfType<AudioManager>().Play("BulletHit");
        Hitpoints -= damage;
        HealthBar.SetHealth(Hitpoints);
        if (Hitpoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
