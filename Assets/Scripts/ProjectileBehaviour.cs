using System;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{

    [SerializeField] private float bulletSpeed = 6f;
    [SerializeField] private float bulletDamage = 1;

    private void Start()
    {
        bulletDamage = 1;
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

    public void IncreaseBulletDamage()
    {
        bulletDamage++;
        print("Damage up! Current damage: " + bulletDamage);
    }
}
