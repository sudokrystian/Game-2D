using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    // Stats
    [SerializeField] private float arrowSpeed = 6f;
    [SerializeField] private int arrowDamage = 2;
    [SerializeField] private float arrowRange = 14f;

    // Audio manager
    private AudioManager audioManager;
    private AudioSource audioSource;
    
    private Vector2 startingPosition;


    private void Start()
    {
        startingPosition = transform.position;
        audioManager = FindObjectOfType<AudioManager>();
        audioSource = audioManager.AttachAudioSourceToGameObject(gameObject, "Arrow");
        audioSource.Play();
    }

    void Update()
    {
        if (Vector2.Distance(startingPosition, transform.position) > arrowRange)
        {
            Destroy(gameObject);
        }
        gameObject.transform.position += -transform.right * Time.deltaTime * arrowSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Character2DController>();
        if (player)
        {
            player.TakeHit(arrowDamage);
        }
        var enemy = collision.collider.GetComponent<EnemyStats>();
        if (enemy)
        {

            enemy.TakeHit(arrowDamage);
        }
        audioManager.DetachAudioSource(audioSource);
        Destroy(gameObject);
    }
}
