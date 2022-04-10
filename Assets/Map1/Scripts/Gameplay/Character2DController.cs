using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Character2DController : MonoBehaviour

{
    // Game Controller
    [SerializeField] private GameController gameController;
    // Audio
    private AudioManager audioManager;

    // Player body
    private Rigidbody2D rigidBody;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private ManaBar manaBar;
    [SerializeField] private GameObject playerGFX;
    [SerializeField] private TextMeshProUGUI magicStats;
    [SerializeField] private TextMeshProUGUI movementStats;
    private TextMeshProUGUI healthBarStats;
    private TextMeshProUGUI manaBarStats;



    // Player actions
    private float horizontalMove = 0;
    private bool jump = false;
    private bool shoot = false;

    // Player base stats
    [SerializeField] private int playerMaxHealth = 8;
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private int playerMaxMana = 5;
    [SerializeField] private float manaRegenerationCooldown = 2.2f;

    private int damage = 1;
    private int hitpoints;
    private int mana;

    //Bullets
    [SerializeField] private ProjectileBehaviour projectilePrefab;
    [SerializeField] private Transform launchOffset;
    
    //Mana timer
    private float timeManaCharging = 0;
    
    // Animations
    private Animator animator;
    private readonly int speedHash = Animator.StringToHash("Speed");
    private readonly int jumpHash = Animator.StringToHash("Jump");
    private readonly int hitHash = Animator.StringToHash("Hit");


    
    private void Start()
    {
        // Initialize values
        rigidBody = GetComponent<Rigidbody2D>();
        audioManager = FindObjectOfType<AudioManager>();
        animator = playerGFX.GetComponent<Animator>();
        healthBarStats = healthBar.GetComponentInChildren<TextMeshProUGUI>();
        manaBarStats = manaBar.GetComponentInChildren<TextMeshProUGUI>();

        // Update player stats in UI
        healthBar.SetMaxHealth(playerMaxHealth);
        hitpoints = playerMaxHealth;
        manaBar.SetMaxMana(playerMaxMana);
        mana = playerMaxMana;
        magicStats.text = Convert.ToString(damage);
        movementStats.text = Convert.ToString(movementSpeed);
        healthBarStats.text = GetHealthStats();
        manaBarStats.text = GetManaStats();
    }

    private void Update()
    {
        SaveActions();
    }

    private void FixedUpdate()
    {
        PerformActions();
        ManaCharging();
    }

    public void TakeHit(int damage)
    {
        // Update UI
        hitpoints -= damage;
        healthBar.SetHealth(hitpoints);
        healthBarStats.text = GetHealthStats();
        // Animate being hit
        animator.SetTrigger(hitHash);
        audioManager.Play("Hurt");
        if (hitpoints <= 0)
        {
            audioManager.Play("Death");
            gameObject.SetActive(false);
            gameController.GameOver();
        }
    }

    public void RecoverHealth(int extraHealth)
    {
        if (hitpoints != playerMaxHealth)
        {
            if ((hitpoints + extraHealth) > playerMaxHealth)
            {
                hitpoints = playerMaxHealth;
                healthBar.SetHealth(hitpoints);
                healthBarStats.text = GetHealthStats();
            }
            else
            {
                gameController.HealthRecoverPopUp(extraHealth);
                hitpoints += extraHealth;
                healthBar.SetHealth(hitpoints);
                healthBarStats.text = GetHealthStats();
            }
        }
    }

    public void IncreaseHealth(int extraHealth)
    {
        gameController.HealthUpPopUp(extraHealth);
        playerMaxHealth += extraHealth;
        hitpoints += extraHealth;
        healthBar.SetMaxHealth(playerMaxHealth);
        healthBar.SetHealth(hitpoints);
        healthBarStats.text = GetHealthStats();
    }
    
    public void IncreaseDamage(int damage)
    {
        gameController.DamageUpPopUp(damage);
        this.damage++;
        magicStats.text = Convert.ToString(this.damage);
    }

    public void IncreaseMana(int extraMana)
    {
        gameController.ManaUp(extraMana);
        playerMaxMana += extraMana;
        mana += extraMana;
        manaBar.SetMaxMana(playerMaxMana);
        manaBar.SetMana(mana);
        manaBarStats.text = GetManaStats();
    }

    public void IncreaseSpeed(float speedBonus)
    {
        gameController.MovementUp(speedBonus);
        movementSpeed += speedBonus;
        movementStats.text = Convert.ToString(movementSpeed);
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
            if (mana > 0)
            {
                shoot = true;
            }
            else
            {
                audioManager.Play("NoMana");
            }
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
            audioManager.Play("Jump");
            audioManager.Play("Frog");
        }

        if (shoot)
        {
            // Create a bullet
            ProjectileBehaviour bullet = Instantiate(projectilePrefab, launchOffset.position, Quaternion.Inverse(transform.rotation));
            bullet.SetDamage(damage);
            // Update mana
            mana--;
            manaBar.SetMana(mana);
            manaBarStats.text = GetManaStats();
            // Update the flag
            shoot = false;
            // Sound effect
            audioManager.Play("Kamehada2");
        }
    }

    private void ManaCharging()
    {
        if (timeManaCharging < manaRegenerationCooldown)
        {
            timeManaCharging += Time.deltaTime;
        }
        else
        {
            if (mana < playerMaxMana)
            {
                mana++;
                manaBar.SetMana(mana);
                manaBarStats.text = GetManaStats();
                timeManaCharging = 0;
            }
        }
    }

    private string GetHealthStats()
    {
        return hitpoints + " / " + playerMaxHealth;
    }

    private string GetManaStats()
    {
        return mana + " / " + playerMaxMana;
    }
}
