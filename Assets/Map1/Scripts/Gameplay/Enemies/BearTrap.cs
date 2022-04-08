using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    [SerializeField] private int bearTrapDamage = 3;
    
    // Animations
    private Animator animator;
    private readonly int activateHash = Animator.StringToHash("Activate");
    private float disapearDelay = 2f;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Character2DController>();
        if (player)
        {
            animator.SetTrigger(activateHash);
            player.TakeHit(bearTrapDamage);
        }
    }
}
