using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2DController : MonoBehaviour

{
    // Game Controller
    [SerializeField] private GameController gameController;

    // Player body
    private Rigidbody2D rigidBody;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject playerGFX;


    // Player actions
    private float horizontalMove = 0;
    private bool jump = false;
    private bool shoot = false;

    // Player base stats
    [SerializeField] private float playerMaxHealth = 8;
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float jumpForce = 7f;
    private float Hitpoints;

    //Bullets
    [SerializeField] private ProjectileBehaviour projectilePrefab;
    [SerializeField] private Transform launchOffset;
    
    // Animations
    private Animator animator;
    private readonly int speedHash = Animator.StringToHash("Speed");
    private readonly int jumpHash = Animator.StringToHash("Jump");
    private readonly int hitHash = Animator.StringToHash("Hit");


    
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        healthBar.SetMaxHealth(playerMaxHealth);
        Hitpoints = playerMaxHealth;
        animator = playerGFX.GetComponent<Animator>();
    }

    private void Update()
    {
        SaveActions();
    }

    private void FixedUpdate()
    {
        PerformActions();
    }

    public void TakeHit(float damage)
    {
        Hitpoints -= damage;
        healthBar.SetHealth(Hitpoints);
        // Animate being hit
        animator.SetTrigger(hitHash);
        if (Hitpoints <= 0)
        {
            gameController.GameOver();
        }
    }

    public void AddHealth(float extraHealth)
    {
        if (Hitpoints != playerMaxHealth)
        {
            if ((Hitpoints + extraHealth) > playerMaxHealth)
            {
                Hitpoints = playerMaxHealth;
                healthBar.SetHealth(Hitpoints);
            }
            else
            {
                Hitpoints += extraHealth;
                healthBar.SetHealth(Hitpoints);
            }
        }

    }

    private void SaveActions()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        this.horizontalMove = horizontalMove;
        
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rigidBody.velocity.y) < 0.001f)
        {
            jump = true;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            shoot = true;
        }
    }

    private void PerformActions()
    {
        transform.position += new Vector3(horizontalMove, 0, 0) * Time.deltaTime * movementSpeed;
        // Set the value for the animation
        if (horizontalMove == 0)
        {
            animator.SetInteger(speedHash, 0);
        }
        else
        {
            animator.SetInteger(speedHash, 1);

        }
        
        if (horizontalMove < 0)
        {
            transform.rotation = Quaternion.identity;
        } else if (horizontalMove > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (jump)
        {
            rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            this.jump = false;
            // Jump animation
            animator.SetTrigger(jumpHash);
        }

        if (shoot)
        {
            Instantiate(projectilePrefab, launchOffset.position, Quaternion.Inverse(transform.rotation));
            shoot = false;
        }
    }

    public void IncreaseDamage()
    {
        gameController.DamageUpPopUp();
        projectilePrefab.IncreaseBulletDamage();
    }

}
