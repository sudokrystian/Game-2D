using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public GameObject lavaProjectile;
    public Transform lavaSpawn;
    private int lavaTimer = 2;
    private int lavaDamage = 99999;
    // Audio manager
    public AudioManager audioManager;
    void Start()
    {
        audioManager.Play("Lava");
        InvokeRepeating("CreateLavaProjectile", lavaTimer, lavaTimer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Character2DController>();
        if (player)
        {
            player.TakeHit(lavaDamage);
        }
    }
    
    private void CreateLavaProjectile()
    {
        // Make sure that the z is correct so the object is visible during the gameplay
        float z = 1;
        Vector3 projectilePosition = new Vector3(lavaSpawn.position.x, lavaSpawn.position.y, z);
        Instantiate(lavaProjectile, projectilePosition, lavaSpawn.rotation);
    }
}
