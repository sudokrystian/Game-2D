using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    // Stats
    [SerializeField] private float arrowSpeed = 6f;
    [SerializeField] private int arrowDamage = 2;
    // Audio manager
    public AudioManager audioManager;


    private void Start()
    {
        audioManager.AttachAudioSourceToGameObject(gameObject, "Arrow").Play();
    }

    void Update()
    {
        gameObject.transform.position += -transform.right * Time.deltaTime * arrowSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Character2DController>();
        if (player)
        {
            player.TakeHit(arrowDamage);
        }
        Destroy(gameObject);
    }
}
