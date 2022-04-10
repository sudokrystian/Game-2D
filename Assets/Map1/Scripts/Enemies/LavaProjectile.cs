using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LavaProjectile : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    [SerializeField] private int lavaDamage = 3;
    [SerializeField] private float impulseForce = 9f;

    private float spawnArc;
    // Audio manager
    private AudioManager audioManager;
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        var x = Random.Range(-spawnArc, spawnArc);
        rigidBody.AddForce(new Vector2(x, impulseForce), ForceMode2D.Impulse);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Character2DController>();
        if (player)
        {
            audioManager.Play("LavaProjectile");
            player.TakeHit(lavaDamage);
        }
        var enemy = collision.collider.GetComponent<EnemyStats>();
        if (enemy)
        {

            enemy.TakeHit(lavaDamage);
        }
        var lava = collision.collider.GetComponent<Lava>();
        if (!lava)
        {
            Destroy(gameObject);
        }
        else
        {
            Physics2D.IgnoreCollision(lava.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }
    }

    public float SpawnArc
    {
        get => spawnArc;
        set => spawnArc = value;
    }
}
