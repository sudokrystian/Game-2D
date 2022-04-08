using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBehaviour : MonoBehaviour
{
    // Enemy components
    private Rigidbody2D rigidBody;
    public Transform enemyGFX;
    public GameObject bloodPrefab;
    [SerializeField] private HealthBar healthBar;
    // Enemy stats
    [SerializeField] private float enemySpeed = 250f;
    [SerializeField] private int enemyMaxHealth = 5;
    [SerializeField] private int enemyDamage = 1;
    // How far before the enemy can see the player
    [SerializeField] private float visionRange = 8;
    private int hitpoints;
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

    //Drops
    public GameObject damageUpDrop;
    [SerializeField] private float damageUpDropChance = 0.07f;
    
    public GameObject healthRecoveryDrop;
    [SerializeField] private float healthRecoveryDropChance = 0.20f;
    
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        hitpoints = enemyMaxHealth;
        healthBar.SetMaxHealth(enemyMaxHealth);
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
            player.TakeHit(enemyDamage);
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
                player.TakeHit(enemyDamage);
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
        Vector2 force = direction * enemySpeed * Time.deltaTime;
        // Move the enemy if he is in range
        float distanceFromPlayer = Vector2.Distance(rigidBody.position, target.position);
        if (distanceFromPlayer < visionRange)
        {
            rigidBody.AddForce(force);
        }
        
        float distance = Vector2.Distance(rigidBody.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (rigidBody.velocity.x >= 0.01f)
        {
            enemyGFX.transform.rotation = Quaternion.Euler(0, 180, 0);

        } else if (rigidBody.velocity.x <= -0.01f)
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
    

    public void TakeHit(int damage)
    {
        var blood = Instantiate(bloodPrefab, rigidBody.transform.position, rigidBody.transform.rotation);
        blood.GetComponent<ParticleSystem>().Play();
        FindObjectOfType<AudioManager>().Play("BulletHit");
        hitpoints -= damage;
        healthBar.SetHealth(hitpoints);
        if (hitpoints <= 0)
        {
            // Random chance for drops
            if (Random.value < damageUpDropChance)
            {
                Instantiate(damageUpDrop, gameObject.transform.position, Quaternion.Inverse(transform.rotation));
            }
            if (Random.value < healthRecoveryDropChance)
            {
                Instantiate(healthRecoveryDrop, gameObject.transform.position, Quaternion.Inverse(transform.rotation));
            }
            Destroy(gameObject);
        }
    }
}
