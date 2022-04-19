using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggresiveGroundedPatrol : MonoBehaviour
{
    [SerializeField] private float patrollingRange;
    private Vector2 startingPosition;
    private Vector2 nextTarget;
    private bool reachedEnd = true;

    private bool waiting = false;

    // Collision info
    private float timeWaiting = 0;

    // Time before damage is taken
    [SerializeField] private float timeWaitingThreshold = 1f;

    public EnemyStats enemyStats;
    public EnemyAI enemyAI;

    void Start()
    {
        startingPosition = gameObject.transform.position;
    }

    void Update()
    {
        if (reachedEnd && !enemyAI.Agitated)
        {
            // The stationary enemies shouldn't be constantly running therefore let's have a waiting period
            // after reaching the target
            if (timeWaiting < timeWaitingThreshold)
            {
                waiting = true;
                timeWaiting += Time.deltaTime;
            }
            else
            {
                waiting = false;
                timeWaiting = 0f;
                var x = startingPosition.x + Random.Range(-patrollingRange, patrollingRange);
                var y = startingPosition.y;
                nextTarget = new Vector2(x, y);
                reachedEnd = false;
            }
        }

        if (Vector2.Distance(gameObject.transform.position, nextTarget) < 0.1f)
        {
            reachedEnd = true;
        }
    }

    private void FixedUpdate()
    {
        // Patrol only when the enemy didn't spot the player
        if (!enemyAI.Agitated)
        {
            if (!waiting)
            {
                // Get the direction and force to move
                var direction = (nextTarget - (Vector2) gameObject.transform.position).normalized;
                var force = direction * enemyStats.EnemySpeed * Time.deltaTime;
                enemyStats.RigidBody.AddForce(force);
            }
        }
        // If not agitated anymore, patrol again
        else
        {
            startingPosition = gameObject.transform.position;
            var x = startingPosition.x + Random.Range(-patrollingRange, patrollingRange);
            var y = startingPosition.y;
            nextTarget = new Vector2(x, y);
            reachedEnd = false;
        }
    }
    
}