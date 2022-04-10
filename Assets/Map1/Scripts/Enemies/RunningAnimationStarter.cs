using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningAnimationStarter : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public GameObject enemyGFX;
    private Animator animator;
    private readonly int runningHash = Animator.StringToHash("isRunning");


    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = enemyGFX.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (rigidBody.velocity.x != 0)
        {
            animator.SetBool(runningHash, true);
        }
        else
        {
            animator.SetBool(runningHash, false);

        }
    }
}
