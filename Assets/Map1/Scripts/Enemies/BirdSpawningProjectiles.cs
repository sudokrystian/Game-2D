using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawningProjectiles : MonoBehaviour
{
    public EnemyStats enemyStats;
    public BirdProjectile projectile;
    public Transform projectileSpawn;
    [SerializeField] private float projectileTimer = 2f;
    
    private void Start()
    {
        InvokeRepeating("CreateProjectile", projectileTimer, projectileTimer);
    }

    private void CreateProjectile()
    {
        // Make sure that the z is correct so the object is visible during the gameplay
        float z = 1;
        // Vector3 projectilePosition = new Vector3((gameObject.transform.position.x + projectileSpawnerXOffset), (gameObject.transform.position.y + projectileSpawnerYOffset), z);
        Vector3 projectilePosition = new Vector3(projectileSpawn.position.x, projectileSpawn.position.y, z);

        var projectile = Instantiate(this.projectile, projectilePosition, projectileSpawn.rotation);
        projectile.ProjectileDamage = enemyStats.EnemyDamage;
    }
}
