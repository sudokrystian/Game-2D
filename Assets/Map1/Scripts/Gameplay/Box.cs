using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    //Stats
    [SerializeField] private float maxHealth = 3f;
    private float health;
    
    // Drop
    public GameObject boxDrop;
    
    // Animations
    private Animator animator;
    private readonly int hitHash = Animator.StringToHash("Hit");
    private readonly int destroyHash = Animator.StringToHash("Destroy");
    private float dissapearDelay = 2f;

    private void Start()
    {
        health = maxHealth;
        animator = gameObject.GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        print("Box taking damage");
        animator.SetTrigger(hitHash);
        health -= damage;
        if (health <= 0)
        {
            print("Box destroying");

            animator.SetTrigger(destroyHash);
            Instantiate(boxDrop, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(
                gameObject, 
                this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + dissapearDelay
                );
        }
    }
}
