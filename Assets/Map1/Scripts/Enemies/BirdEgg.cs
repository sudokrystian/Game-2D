using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEgg : MonoBehaviour
{
    public GameObject eggProjectile;
    [SerializeField] private float eggSpeed = 6f;
    private int eggDamage = 2;
    // Audio manager
    private AudioManager audioManager;
    void Update()
    {
        audioManager = FindObjectOfType<AudioManager>();
        eggProjectile.transform.position += -transform.up * Time.deltaTime * eggSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Character2DController>();
        if (player)
        {
            audioManager.Play("EggCrack");
            player.TakeHit(eggDamage);
        }
        var enemy = collision.collider.GetComponent<EnemyStats>();
        if (enemy)
        {

            enemy.TakeHit(eggDamage);
        }
        Destroy(gameObject);
    }

    public int EggDamage
    {
        get => eggDamage;
        set => eggDamage = value;
    }
}
