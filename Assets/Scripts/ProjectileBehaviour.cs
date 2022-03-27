using System;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{

    [SerializeField] private float BulletSpeed = 6f;
    private float BulletDamage = 1;

    private void Update()
    {
        transform.position += -transform.right * Time.deltaTime * BulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var enemy = col.collider.GetComponent<EnemyBehaviour>();
        if (enemy)
        {
            enemy.TakeHit(BulletDamage);
        }
        Destroy(gameObject);
    }
}
