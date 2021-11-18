using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyCharger : Enemy
{
    // Private Attributes
    private Vector2 directionOnChargeStart;

    private float currentSpeed;
    private float currentAttackRecoverTime;
    private float currentChargeTime;
    private bool hasRecovered;
    private bool collidedWithPlayer;

    // Public Attributes
    public const float ATTACK_RECOVER_TIME = 2f;
    public const float CHARGE_SPEED = 12f;
    public const float CHARGE_TIME = 0.5f;
    public const float MAX_SPEED = 7f;
    public const float ACCELERATION = 0.25f;

    public float attackForce = 8f;
    public float distanceToCharge = 4f;

    private void Start()
    {
        attackSystem = GetComponent<AttackSystem>();
        healthSystem = GetComponent<HealthSystem>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");

        currentAttackRecoverTime = ATTACK_RECOVER_TIME;
        currentChargeTime = CHARGE_TIME;
        currentSpeed = 1f;
        hasRecovered = false;
        collidedWithPlayer = false;
        enemyState = EnemyState.AGGRO;
        attackState = AttackState.MOVING_TOWARDS_PLAYER;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSystem.IsDead())
        {
            Die();
        }


        if (enemyState == EnemyState.AGGRO)
        {
            if (attackState == AttackState.MOVING_TOWARDS_PLAYER)
            {
                if (currentSpeed < MAX_SPEED)
                {
                    currentSpeed += ACCELERATION;
                }
                else
                {
                    currentSpeed = MAX_SPEED;
                }

                // Change to CHARGE
                if (Vector2.Distance(transform.position, player.transform.position) <= distanceToCharge)
                {
                    UpdatePlayerPosition();
                    directionOnChargeStart = (playerPosition - rigidbody.position).normalized;
                    attackState = AttackState.CHARGING; // Change state
                }
            }
            else if (attackState == AttackState.CHARGING)
            {
                spriteRenderer.color = new Color(1f, 0f, 0f, 1f);
                if (collidedWithPlayer)
                {
                    collidedWithPlayer = false;
                    currentChargeTime = CHARGE_TIME; // Reset value
                    spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // Reset color
                    attackState = AttackState.RECOVERING; // Change state
                }
            }
            else if (attackState == AttackState.RECOVERING)
            {
                spriteRenderer.color = new Color(0.36f, 0.36f, 0.36f, 1f);
                currentSpeed = 0f;
                Recovering();
            }
        }
    }



    private void FixedUpdate()
    {
        if (attackState == AttackState.MOVING_TOWARDS_PLAYER)
        {
            MoveTowardsPlayer();
        }
        else if (attackState == AttackState.CHARGING)
        {
            if (collidedWithPlayer)
            {
                PushPlayerAndSelf();
            }
            else
            {
                Charge();
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            DamagePlayer();
            collidedWithPlayer = true;
        }
    }

    private void MoveTowardsPlayer()
    {
        UpdatePlayerPosition();
        UpdateDirectionTowardsPlayerPosition();

        rigidbody.MovePosition((Vector2)transform.position + directionTowardsPlayerPosition * (currentSpeed * Time.deltaTime));
    }

    private void Charge()
    {
        rigidbody.MovePosition((Vector2)transform.position + directionOnChargeStart * (CHARGE_SPEED * Time.deltaTime));

        currentChargeTime -= Time.deltaTime;
        if (currentChargeTime <= 0)
        {
            currentChargeTime = CHARGE_TIME; // Reset value
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // Reset color
            attackState = AttackState.RECOVERING; // Change state
        }
    }

    private void PushPlayerAndSelf()
    {
        Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerRigidbody.AddForce(directionOnChargeStart * attackForce, ForceMode2D.Impulse);
        rigidbody.AddForce(-directionOnChargeStart * attackForce, ForceMode2D.Impulse);
    }

    private void Recovering()
    {
        currentAttackRecoverTime -= Time.deltaTime;
        rigidbody.bodyType = RigidbodyType2D.Static;

        if (currentAttackRecoverTime <= 0)
        {
            hasRecovered = true;

            rigidbody.bodyType = RigidbodyType2D.Dynamic;
            currentAttackRecoverTime = ATTACK_RECOVER_TIME; // Reset value
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // Reset color
            attackState = AttackState.MOVING_TOWARDS_PLAYER; // Change state
        }
    }
}
