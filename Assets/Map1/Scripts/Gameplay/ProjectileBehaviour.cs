using System;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{

    private float bulletSpeed = 6f;
    private int bulletDamage = 1;
    
    private void Update()
    {
        transform.position += -transform.right * Time.deltaTime * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var enemy = col.collider.GetComponent<EnemyBehaviour>();
        if (enemy)
        {

            enemy.TakeHit(bulletDamage);
        }
        Destroy(gameObject);
    }



    public void SetDamage(int dmg)
    {
        this.bulletDamage = dmg;
    }
}
