using System;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    // Stats
    private float bulletSpeed = 6f;
    private int bulletDamage = 1;

    private void Update()
    {
        transform.position += -transform.right * Time.deltaTime * bulletSpeed;
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
}
