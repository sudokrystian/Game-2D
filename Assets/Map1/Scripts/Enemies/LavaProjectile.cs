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
    // Audio manager
    public AudioManager audioManager;
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        var x = Random.Range(-5f, 5f);
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

        var lava = collision.collider.GetComponent<Lava>();
        if (!lava)
        {
            Destroy(gameObject);
        }
    }
}
