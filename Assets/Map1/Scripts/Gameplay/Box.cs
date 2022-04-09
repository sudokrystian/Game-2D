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

    // Audio manager
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        health = maxHealth;
        animator = gameObject.GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        audioManager.Play("BoxHit");
        animator.SetTrigger(hitHash);
        health -= damage;
        if (health <= 0)
        {
            audioManager.Play("BoxDestroyed");
            animator.SetTrigger(destroyHash);
            Instantiate(boxDrop, gameObject.transform.position, gameObject.transform.rotation);
        }
    }
}
