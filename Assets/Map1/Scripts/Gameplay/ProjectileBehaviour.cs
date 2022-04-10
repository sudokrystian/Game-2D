using System;
using System.Security.Cryptography;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    // Stats
    private float bulletSpeed = 6f;
    private int bulletDamage = 1;
    [SerializeField] private float bulletRange = 7f;
    private bool up;
    private bool down;
    private bool horizontal;
    private Vector2 startingPosition;

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        if (Vector2.Distance(startingPosition, transform.position) > bulletRange)
        {
            Destroy(gameObject);
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

        Destroy(gameObject, 0.01f);
    }



    public void SetDamage(int dmg)
    {
        this.bulletDamage = dmg;
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
