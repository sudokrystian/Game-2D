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
    [SerializeField] protected float visionRange = 8;

    // Enemy flags
    protected bool agitated = false;
    protected bool confused = false;
    protected Vector2 randomDirection;

    // Target for AI
    protected Transform target;
    [SerializeField] protected float nextWaypointDistance = 1f;

    protected Path path;
    protected int currentWaypoint = 0;
    protected bool reachedEndOfPath = false;
    protected Seeker seeker;

    void Start()
    {
        target = GameObject.FindWithTag("PlayerHitbox").GetComponent<RectTransform>();
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
        
        EnemyPathfinding();
    }

    // Overwrite in the inherited classes
    public virtual void EnemyPathfinding()
    {
        throw new NotImplementedException();
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

    public bool Agitated => agitated;
}