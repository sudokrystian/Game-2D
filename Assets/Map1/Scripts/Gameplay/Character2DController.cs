using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Character2DController : MonoBehaviour

{
    // Game Controller
    private GameController gameController;
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
    private bool lookingUp = false;
    private bool lookingDown = false;

    // Player base stats
    [SerializeField] private int playerMaxHealth = 8;
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private int playerMaxMana = 5;
    [SerializeField] private float manaRegenerationCooldown = 2.2f;
    [SerializeField] private float bulletChargingCooldown = 0.4f;

    private int damage = 1;
    private int hitpoints;
    private int mana;

    //Bullets
    [SerializeField] private ProjectileBehaviour projectilePrefab;
    [SerializeField] private Transform horizontalLaunchOffset;
    [SerializeField] private Transform upLaunchOffset;
    [SerializeField] private Transform downLaunchOffset;

    // Bullet timer
    private bool canShoot = true;
    private float timeBulletCharging = 0;
    
    // Mana timer
    private bool manaCharged = false;
    private float timeManaCharging = 0;
    
    // Animations
    private Animator animator;
    private readonly int speedHash = Animator.StringToHash("Speed");
    private readonly int jumpHash = Animator.StringToHash("Jump");
    private readonly int hitHash = Animator.StringToHash("Hit");

    public Animation animation;
    
    private void Start()
    {
        // Initialize values
        gameController = FindObjectOfType<GameController>();
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
        // Set the initial length for bullet charging time
        gameController.SetBulletAnimationLength(bulletChargingCooldown);

    }

    private void Update()
    {
        BulletCharging();
        SaveActions();
        ManaCharging();
    }

    private void FixedUpdate()
    {
        PerformActions();
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
        float verticalAxis = Input.GetAxisRaw("Vertical");
        if (verticalAxis > 0)
        {
            lookingUp = true;
            lookingDown = true;
        }
        else if (verticalAxis < 0)
        {
            lookingDown = true;
            lookingUp = false;
        }
        else
        {
            lookingUp = false;
            lookingDown = false;
        }
        
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rigidBody.velocity.y) < 0.001f)
        {
            jump = true;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if (mana > 0 && canShoot)
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
            // Play UI animation
            gameController.PlayLoadBulletAnimation();
            canShoot = false;
            // Create a bullet up, down or horizontal
            ProjectileBehaviour bullet;
            if (lookingUp)
            {
                bullet = Instantiate(projectilePrefab, upLaunchOffset.position, Quaternion.Inverse(transform.rotation));
                bullet.Up();
            }
            else if(lookingDown)
            {
                bullet = Instantiate(projectilePrefab, downLaunchOffset.position, Quaternion.Inverse(transform.rotation));
                bullet.Down();
            }
            else
            {
                bullet = Instantiate(projectilePrefab, horizontalLaunchOffset.position, Quaternion.Inverse(transform.rotation));
                bullet.Horizontal();
            }
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

        if (manaCharged)
        {
            mana++;
            manaBar.SetMana(mana);
            manaBarStats.text = GetManaStats();
            manaCharged = false;
        }
    }
    
    private void BulletCharging()
    {
        if (!canShoot)
        {
            if (timeBulletCharging < bulletChargingCooldown)
            {
                timeBulletCharging += Time.deltaTime;
            }
            else
            {
                canShoot = true;
                timeBulletCharging = 0;
            }
        }
    }
    
    private void ManaCharging()
    {
        if (!manaCharged)
        {
            if (timeManaCharging < manaRegenerationCooldown)
            {
                timeManaCharging += Time.deltaTime;
            }
            else
            {
                if (mana < playerMaxMana)
                {
                    timeManaCharging = 0;
                    manaCharged = true;
                }
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
