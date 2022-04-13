using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdProjectile : MonoBehaviour
{
    public GameObject projectile;
    [SerializeField] private float projectileSpeed = 6f;
    private int projectileDamage = 1;

    void Update()
    {
        projectile.transform.position += -transform.up * Time.deltaTime * projectileSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Character2DController>();
        if (player)
        {
            OnPlayerHitActions();
            player.TakeHit(projectileDamage);
        }
        var enemy = collision.collider.GetComponent<EnemyStats>();
        if (enemy)
        {

            enemy.TakeHit(projectileDamage);
        }
        Destroy(gameObject);
    }

    public virtual void OnPlayerHitActions()
    {
        
    }

    public int ProjectileDamage
    {
        get => projectileDamage;
        set => projectileDamage = value;
    }
}
