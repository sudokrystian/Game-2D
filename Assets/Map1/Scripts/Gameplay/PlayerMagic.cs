using System;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMagic : MonoBehaviour
{
    // Stats
    private float bulletSpeed = 6f;
    // Assigned by player stats
    private int bulletDamage;
    private float bulletRange;
    
    private bool up;
    private bool down;
    private bool horizontal;
    private Vector2 startingPosition;
   
    public GameObject destroyedBullet;


    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        if (Vector2.Distance(startingPosition, transform.position) > bulletRange)
        {
            DestroyTheBullet();
        }

        if (up)
        {
            transform.position += transform.up * Time.deltaTime * bulletSpeed;

        }

        if (horizontal)
        {
            transform.position += -transform.right * Time.deltaTime * bulletSpeed;

        }

        if (down)
        {
            transform.position += -transform.up * Time.deltaTime * bulletSpeed;

        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // Enemy hit
        var enemy = col.collider.GetComponent<EnemyStats>();
        if (enemy)
        {
            enemy.TakeHit(bulletDamage);
        }
        // Box hit
        var box = col.collider.GetComponent<Box>();
        if (box)
        {
            box.TakeDamage(bulletDamage);
        }
        DestroyTheBullet();
    }
    
    private void DestroyTheBullet() {
        Instantiate(destroyedBullet, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }



    public void SetDamage(int dmg)
    {
        this.bulletDamage = dmg;
    }
    
    public float BulletRange
    {
        get => bulletRange;
        set => bulletRange = value;
    }

    public void Horizontal()
    {
        horizontal = true;
    }

    public void Up()
    {
        up = true;
    }

    public void Down()
    {
        down = true;
    }
}
