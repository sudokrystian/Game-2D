using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundedEnemyAI : EnemyAI
{
    private bool canJump = false;

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
    
    public override void EnemyPathfinding()
    {
        // Get the direction and force to move
        var x = path.vectorPath[currentWaypoint].x - enemyStats.RigidBody.position.x;
        var direction = new Vector2(x, 0).normalized;
        var force = direction * enemyStats.EnemySpeed * Time.deltaTime;

        // Move the enemy if he is in range
        float distanceFromPlayer = Vector2.Distance(enemyStats.RigidBody.position, target.position);
        if (distanceFromPlayer < base.visionRange)
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

    private void CheckIfGrounded()
    {
        if (Mathf.Abs(enemyStats.RigidBody.velocity.y) < 0.001f)
        {
            canJump = true;
        }
    }
}
