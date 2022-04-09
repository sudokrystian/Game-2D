using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class EnemyAI : MonoBehaviour
{
    // Enemy
    public EnemyStats enemyStats;
    // How far before the enemy can see the player
    [SerializeField] private float visionRange = 8;
    // Target for AI
    public Transform target;
    public float nextWaypointDistance = 1f;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    private Seeker seeker;
    void Start()
    {
        seeker = GetComponent<Seeker>();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    private void FixedUpdate()
    {
        MoveEnemyTowardsTarget();
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
        Vector2 direction = ((Vector2) path.vectorPath[currentWaypoint] - (Vector2) enemyStats.RigidBody.position).normalized;
        Vector2 force = direction * enemyStats.EnemySpeed * Time.deltaTime;
        // Move the enemy if he is in range
        float distanceFromPlayer = Vector2.Distance(enemyStats.RigidBody.position, target.position);
        if (distanceFromPlayer < visionRange)
        {
            enemyStats.RigidBody.AddForce(force);
        }
        
        float distance = Vector2.Distance(enemyStats.RigidBody.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (enemyStats.RigidBody.velocity.x >= 0.01f)
        {
            enemyStats.enemyGFX.transform.rotation = Quaternion.Euler(0, 180, 0);

        } else if (enemyStats.RigidBody.velocity.x <= -0.01f)
        {
            enemyStats.enemyGFX.transform.rotation = Quaternion.identity;
        }
    }
    
    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(enemyStats.RigidBody.position, target.position, OnPathComplete);
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
}
