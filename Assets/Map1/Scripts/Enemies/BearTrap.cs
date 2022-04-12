using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    [SerializeField] private int bearTrapDamage = 3;
    
    // Animations
    private Animator animator;
    private readonly int activateHash = Animator.StringToHash("Activate");
    // Audio manager
    private AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        animator = gameObject.GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Character2DController>();
        if (player)
        {
            audioManager.PlaySoundEffect("BearTrap");
            animator.SetTrigger(activateHash);
            player.TakeHit(bearTrapDamage);
        }
        var enemy = collision.collider.GetComponent<EnemyStats>();
        if (enemy)
        {
            audioManager.PlaySoundEffect("BearTrap");
            animator.SetTrigger(activateHash);
            enemy.TakeHit(bearTrapDamage);
        }
    }
}
