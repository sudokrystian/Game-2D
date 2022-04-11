using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Lava : MonoBehaviour
{
    public LavaProjectile lavaProjectile;
    public Transform lavaSpawn;
    [SerializeField] private float lavaTimer = 2.4f;
    private int lavaDamage = 99999;
    // Audio manager
    private AudioManager audioManager;
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        PlaySoundEffect();
        InvokeRepeating("CreateLavaProjectile", lavaTimer, lavaTimer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Character2DController>();
        if (player)
        {
            player.TakeHit(lavaDamage);
        }
        var enemy = collision.collider.GetComponent<EnemyStats>();
        if (enemy)
        {

            enemy.TakeHit(lavaDamage);
        }
    }
    
    private void CreateLavaProjectile()
    {
        // Make sure that the z is correct so the object is visible during the gameplay
        float z = 1;
        Vector3 projectilePosition = new Vector3(lavaSpawn.position.x, lavaSpawn.position.y, z);
        Instantiate(lavaProjectile, projectilePosition, lavaSpawn.rotation);
    }

    private void PlaySoundEffect()
    {
       AudioSource audioSource =  audioManager.AttachAudioSourceToGameObject(gameObject, "Lava");
       audioSource.loop = true;
       audioSource.Play();
    }


}
