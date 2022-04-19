using System;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundedEnemyAI : EnemyAI
{
    private bool canJump = false;
    
    private void Update()
    {
        CheckIfGrounded();
    }
    
    public override void MoveTowardsTheTarget()
    {
        // Move the enemy if he is in range
        float distanceFromPlayer = Vector2.Distance(enemyStats.RigidBody.position, target.position);
        if (distanceFromPlayer < base.visionRange)
        {
            // Mark the enemy as agitated
            agitated = true;
            
            // If the enemy is underneath the player and can jump he will try it
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
                    // Get the direction and force to move
                    var x = path.vectorPath[currentWaypoint].x - enemyStats.RigidBody.position.x;
                    var direction = new Vector2(x, 0).normalized;
                    var force = direction * enemyStats.EnemySpeed * Time.deltaTime;
                    enemyStats.RigidBody.AddForce(force);
                }
                else
                {
                    if (!confused)
                    {
                        // Set a new random direction
                        float randomX = Random.Range(-1f, 1f);
                        randomDirection = new Vector2(randomX, 0).normalized;
                    }
                    // The enemy will start running into the new direction (which will result in updating the waypoints)
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
        
        // Rotate the enemy corresponding to his velocity
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
