using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonAgressivePatrol : MonoBehaviour
{
    [SerializeField] private float patrollingRange;
    private Vector2 startingPosition;
    private Vector2 nextTarget;
    private bool reachedEnd = true;
    public EnemyStats enemyStats;
    private bool movingLeft = true;

    void Start()
    {
        startingPosition = gameObject.transform.position;
    }

    void Update()
    {
        if (reachedEnd)
        {
            var x = startingPosition.x + Random.Range(-patrollingRange, patrollingRange);
            var y = startingPosition.y;
            nextTarget = new Vector2(x, y);
            // Get the direction and force to move
            Vector2 direction = (nextTarget - (Vector2) gameObject.transform.position).normalized;
            // Rotate the enemy to the new direction
            if (direction.x > 0)
            {
                enemyStats.enemyGFX.transform.rotation = Quaternion.Euler(0, 180, 0);

            }
            else if(direction.x < 0)
            {
                enemyStats.enemyGFX.transform.rotation = Quaternion.identity;

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
        Vector2 direction = (nextTarget - (Vector2) gameObject.transform.position).normalized;
        gameObject.transform.position += new Vector3(direction.x, 0, 0) * Time.deltaTime * enemyStats.EnemySpeed;
    }
}
