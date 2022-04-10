using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Random = UnityEngine.Random;


public class EnemyAI : MonoBehaviour
{
    // Enemy
    public EnemyStats enemyStats;

    // How far before the enemy can see the player
    [SerializeField] private float visionRange = 8;

    // Enemy flags
    [SerializeField] private bool canFly = false;
    private bool canJump = false;
    private bool agitated = false;
    private bool confused = false;
    private Vector2 randomDirection;

    // Target for AI
    private Transform target;
    [SerializeField] private float nextWaypointDistance = 1f;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    private Seeker seeker;

    void Start()
    {
        target = GameObject.FindWithTag("PlayerHitbox").GetComponent<RectTransform>();
        seeker = GetComponent<Seeker>();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    private void Update()
    {
        CheckIfGrounded();
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
        
        // Flying enemies
        if (canFly)
        {
            FlyingEnemiesPathFinding();
        }
        // Non-flying enemies
        else
        {
            NonFlyingEnemiesPathFinding();
        }
    }

    private void FlyingEnemiesPathFinding()
    {
        // Get the direction and force to move
        var direction = ((Vector2) path.vectorPath[currentWaypoint] - (Vector2) enemyStats.RigidBody.position)
            .normalized;
        var force = direction * enemyStats.EnemySpeed * Time.deltaTime;
        // Move the enemy if he is in range
        float distanceFromPlayer = Vector2.Distance(enemyStats.RigidBody.position, target.position);
        if (distanceFromPlayer < visionRange)
        {
            agitated = true;
            enemyStats.RigidBody.AddForce(force);
        }
        else
        {
            agitated = false;
        }

        float distance = Vector2.Distance(enemyStats.RigidBody.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (enemyStats.RigidBody.velocity.x >= 0.01f)
        {
            enemyStats.enemyGFX.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (enemyStats.RigidBody.velocity.x <= -0.01f)
        {
            enemyStats.enemyGFX.transform.rotation = Quaternion.identity;
        }
    }

    private void NonFlyingEnemiesPathFinding()
    {
        // Get the direction and force to move
        var x = path.vectorPath[currentWaypoint].x - enemyStats.RigidBody.position.x;
        var direction = new Vector2(x, 0).normalized;
        var force = direction * enemyStats.EnemySpeed * Time.deltaTime;

        // Move the enemy if he is in range
        float distanceFromPlayer = Vector2.Distance(enemyStats.RigidBody.position, target.position);
        if (distanceFromPlayer < visionRange)
        {
            agitated = true;
            // If the enemy is above you and you can jump let's jump
            if (path.vectorPath[currentWaypoint].y > (enemyStats.RigidBody.position.y + 1f) && canJump)
            {
                Vector2 jumpDirection = new Vector2(0, enemyStats.JumpForce);
                enemyStats.RigidBody.AddForce(jumpDirection, ForceMode2D.Impulse);
                this.canJump = false;
            }
            // Otherwise let's walk
            else
            {
                if (Math.Abs(path.vectorPath[currentWaypoint].x - (enemyStats.RigidBody.position.x)) > 0.5f)
                {
                    confused = false;
                    enemyStats.RigidBody.AddForce(force);
                }
                // If the enemy is right underneath try walking left and right to find a new path
                else
                {
                    if (!confused)
                    {
                        float randomX = Random.Range(-1f, 1f);
                        randomDirection = new Vector2(randomX, 0).normalized;
                    }

                    confused = true;
                    Vector2 alternativeForce = randomDirection * enemyStats.EnemySpeed * Time.deltaTime;
                    enemyStats.RigidBody.AddForce(alternativeForce);
                }
            }
        }
        else
        {
            agitated = false;
        }

        float distance = Vector2.Distance(enemyStats.RigidBody.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            // Make sure that we won't go out of bounds when the enemy is close ot the destination
            if (currentWaypoint < path.vectorPath.Count - 1)
            {
                currentWaypoint++;
            }
        }

        if (enemyStats.RigidBody.velocity.x >= 0.01f &&
            (Math.Abs(path.vectorPath[currentWaypoint].x - enemyStats.RigidBody.position.x) > 0.1f))
        {
            enemyStats.enemyGFX.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (enemyStats.RigidBody.velocity.x <= -0.01f &&
                 Math.Abs(path.vectorPath[currentWaypoint].x - enemyStats.RigidBody.position.x) > 0.1f)
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

    private void CheckIfGrounded()
    {
        if (Mathf.Abs(enemyStats.RigidBody.velocity.y) < 0.001f)
        {
            canJump = true;
        }
    }

    public bool Agitated => agitated;
}