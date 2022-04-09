using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    // Stats
    public ArrowProjectile projectile;
    public Transform projectileSpawn;
    [SerializeField] private float arrowTimer = 2f;
    [SerializeField] private int spikesDamage = 2;
    // Audio manager
    private AudioManager audioManager;
    private AudioSource audioSource;
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioSource = audioManager.AttachAudioSourceToGameObject(gameObject, "Indestructible");
        InvokeRepeating("CreateArrowProjectile", arrowTimer, arrowTimer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Character2DController>();
        if (player)
        {
            player.TakeHit(spikesDamage);
        }
        var enemy = collision.collider.GetComponent<EnemyStats>();
        if (enemy)
        {

            enemy.TakeHit(spikesDamage);
        }
        var projectile = collision.collider.GetComponent<ProjectileBehaviour>();
        if (projectile)
        {
            audioSource.Play();
        }

    }

    private void CreateArrowProjectile()
    {
        // Make sure that the z is correct so the object is visible during the gameplay
        float z = 1;
        Vector3 projectilePosition = new Vector3(projectileSpawn.position.x, projectileSpawn.position.y, z);
        Instantiate(projectile, projectilePosition, projectileSpawn.rotation);
    }
}
