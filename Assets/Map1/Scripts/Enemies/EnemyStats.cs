using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyStats : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public Transform enemyGFX;
    public GameObject bloodPrefab;
    [SerializeField] private HealthBar healthBar;
    // Enemy stats
    [SerializeField] private float enemySpeed = 250f;
    [SerializeField] private int enemyMaxHealth = 5;
    [SerializeField] private int enemyDamage = 1;
    [SerializeField] private float jumpForce = 4f;

    private int hitpoints;
    // Collision info
    private float timeColliding = 0;
    // Time before damage is taken
    private float timeThreshold = 0.5f;
    // Drops
    public GameObject drop;
    [SerializeField] private float dropChance = 0.07f;
    // Audio Manager
    private AudioManager audioManager;
    private AudioSource hitSound;
    private AudioSource deathSound;
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        hitSound = audioManager.AttachAudioSourceToGameObject(gameObject, "Hurt");
        deathSound = audioManager.AttachAudioSourceToGameObject(gameObject, "Disappear");
        rigidBody = GetComponent<Rigidbody2D>();
        hitpoints = enemyMaxHealth;
        healthBar.SetMaxHealth(enemyMaxHealth);
        healthBar.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!healthBar.gameObject.activeInHierarchy && hitpoints < enemyMaxHealth)
        {
            healthBar.gameObject.SetActive(true);
        }
    }

    public Rigidbody2D RigidBody => rigidBody;

    public float EnemySpeed => enemySpeed;

    public int EnemyDamage => enemyDamage;

    public Transform EnemyGfx => enemyGFX;

    public float JumpForce => jumpForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Character2DController>();
        if (player)
        {
            player.TakeHit(enemyDamage);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Deal damage every 0.5sec when touching
        var player = collision.collider.GetComponent<Character2DController>();
        if (player)
        {
            if (timeColliding < timeThreshold)
            {
                timeColliding += Time.deltaTime;
            }
            else
            {
                player.TakeHit(enemyDamage);
                timeColliding = 0f;
            }
        }
    }

    public void TakeHit(int damage)
    {
        var blood = Instantiate(bloodPrefab, rigidBody.transform.position, rigidBody.transform.rotation);
        hitSound.maxDistance = 20;
        hitSound.Play();
        hitpoints -= damage;
        healthBar.SetHealth(hitpoints);
        if (hitpoints <= 0)
        {
            deathSound.maxDistance = 20;
            deathSound.Play();
            // Random chance for drops
            if (Random.value < dropChance)
            {
                var spawnPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 1);
                Instantiate(drop, spawnPosition, Quaternion.Inverse(transform.rotation));
            }
            audioManager.DetachAudioSource(hitSound);
            audioManager.DetachAudioSource(deathSound);
            Destroy(gameObject);
        }
    }
}
