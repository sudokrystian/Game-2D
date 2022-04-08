using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBomber : MonoBehaviour
{
    public BirdEgg projectile;
    public Transform projectileSpawn;
    [SerializeField] private float bombTimer = 2f;

    private void Start()
    {
        InvokeRepeating("CreateEggProjectile", bombTimer, bombTimer);

    }

    private void CreateEggProjectile()
    {
        // Make sure that the z is correct so the object is visible during the gameplay
        float z = 1;
        Vector3 projectilePosition = new Vector3(projectileSpawn.position.x, projectileSpawn.position.y, z);
        Instantiate(projectile, projectilePosition, projectileSpawn.rotation);
    }
}
