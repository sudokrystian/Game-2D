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
    private int hitpoints;
    // Collision info
    private float timeColliding = 0;
    // Time before damage is taken
    private float timeThreshold = 0.5f;
    //Drops
    public GameObject drop;
    [SerializeField] private float dropChance = 0.07f;
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        hitpoints = enemyMaxHealth;
        healthBar.SetMaxHealth(enemyMaxHealth);
    }

    public Rigidbody2D RigidBody => rigidBody;

    public float EnemySpeed => enemySpeed;

    public int EnemyDamage => enemyDamage;

    public Transform EnemyGfx => enemyGFX;
    
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
        blood.GetComponent<ParticleSystem>().Play();
        FindObjectOfType<AudioManager>().Play("BulletHit");
        hitpoints -= damage;
        healthBar.SetHealth(hitpoints);
        if (hitpoints <= 0)
        {
            // Random chance for drops
            if (Random.value < dropChance)
            {
                Instantiate(drop, gameObject.transform.position, Quaternion.Inverse(transform.rotation));
            }
            Destroy(gameObject);
        }
    }
}
