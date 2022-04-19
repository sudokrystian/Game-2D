using UnityEngine;

public class FlyingEnemyAI : EnemyAI
{
    public override void EnemyPathfinding()
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
}
