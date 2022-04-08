using System;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{

    private float bulletSpeed = 6f;
    private int bulletDamage = 1;

    private void Start()
    {
        print("Start damage: " + bulletDamage);
    }

    private void Update()
    {
        transform.position += -transform.right * Time.deltaTime * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var enemy = col.collider.GetComponent<EnemyBehaviour>();
        if (enemy)
        {
            print("Enemy hit! Current damage: " + bulletDamage);

            enemy.TakeHit(bulletDamage);
        }
        Destroy(gameObject);
    }



    public void SetDamage(int dmg)
    {
        this.bulletDamage = dmg;
    }
}
