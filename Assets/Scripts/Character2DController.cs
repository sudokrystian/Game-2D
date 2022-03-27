using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2DController : MonoBehaviour

{
    // Game Controller
    [SerializeField] private GameController GameController;
    
    // Player body
    private Rigidbody2D rigidBody;
    [SerializeField] private HealthBar HealthBar;

    
    // Player actions
    private float horizontalMove = 0;
    private bool jump = false;
    private bool shoot = false;

    // Player base stats
    [SerializeField] private float PlayerMaxHealth = 8;
    [SerializeField] private float MovementSpeed = 4f;
    [SerializeField] private float JumpForce = 6f;
    private float Hitpoints;
    
    //Bullets
    [SerializeField] private ProjectileBehaviour ProjectilePrefab;
    [SerializeField] private Transform LaunchOffset;


    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        HealthBar.SetMaxHealth(PlayerMaxHealth);
        Hitpoints = PlayerMaxHealth;
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
        HealthBar.SetHealth(Hitpoints);
        if (Hitpoints <= 0)
        {
            GameController.GameOver();
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
        transform.position += new Vector3(horizontalMove, 0, 0) * Time.deltaTime * MovementSpeed;

        if (horizontalMove < 0)
        {
            // transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.identity;
            // transform.Rotate(0f,0f,0f);
        } else if (horizontalMove > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            // transform.Rotate(0f,180f,0f);
        }

        if (jump)
        {
            rigidBody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            this.jump = false;
        }

        if (shoot)
        {
            print("Shooting the bullet!");
            Instantiate(ProjectilePrefab, LaunchOffset.position, Quaternion.Inverse(transform.rotation));
            // Instantiate(, LaunchOffset.position, transform.rotation);

            shoot = false;
        }
    }



}
