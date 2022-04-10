using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPatrol : MonoBehaviour
{
    [SerializeField] private float patrollingRange;
    private Vector2 startingPosition;
    private Vector2 nextTarget;
    private bool reachedEnd = true;
    public EnemyStats enemyStats;
    public EnemyAI enemyAI;

    void Start()
    {
        startingPosition = gameObject.transform.position;
    }

    void Update()
    {
        if (reachedEnd)
        {
            var x = startingPosition.x + Random.Range(-patrollingRange, patrollingRange);
            var y = startingPosition.y + Random.Range(-patrollingRange, patrollingRange);
            nextTarget = new Vector2(x, y);
            reachedEnd = false;
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
            // Get the direction and force to move
            var direction = (nextTarget - (Vector2) gameObject.transform.position).normalized;
            var force = direction * enemyStats.EnemySpeed * Time.deltaTime;
            float distanceFromTarget = Vector2.Distance(gameObject.transform.position, nextTarget);
            enemyStats.RigidBody.AddForce(force);
        }
    }
}