using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Patrolling : MonoBehaviour
{
    [SerializeField] private bool canFly;
    [SerializeField] private float patrollingRange;
    private Vector2 startingPosition;
    private Vector2 nextTarget;
    private bool reachedEnd = true;

    public EnemyStats enemyStats;
    void Start()
    {
        startingPosition = gameObject.transform.position;
    }

    void Update()
    {
        if (reachedEnd)
        {
            if (canFly)
            {
                var x = startingPosition.x + Random.Range(-patrollingRange, patrollingRange);
                var y = startingPosition.y + Random.Range(-patrollingRange, patrollingRange);
                nextTarget = new Vector2(x, y);

            }
            else
            {
                var x = startingPosition.x + Random.Range(-patrollingRange, patrollingRange);
                var y = startingPosition.y;
                nextTarget = new Vector2(x, y);

            }
            reachedEnd = false;
        }
        if (Vector2.Distance(gameObject.transform.position, nextTarget) < 0.1f)
        {
            reachedEnd = true;
        }
    }

    private void FixedUpdate()
    {
        // Get the direction and force to move
        var direction = (nextTarget - (Vector2) gameObject.transform.position).normalized;
        var force = direction * enemyStats.EnemySpeed * Time.deltaTime;
        float distanceFromTarget = Vector2.Distance(gameObject.transform.position, nextTarget);
        enemyStats.RigidBody.AddForce(force);
    }
}
