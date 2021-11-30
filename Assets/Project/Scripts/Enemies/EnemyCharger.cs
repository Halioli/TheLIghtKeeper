using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


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
    public float CHARGE_SPEED;
    public const float CHARGE_TIME = 0.5f;
    public float MAX_SPEED;
    public const float ACCELERATION = 0.25f;

    public float attackForce = 8f;
    public float distanceToCharge = 4f;

    // Sinusoidal movement
    public float amplitude = 0.1f;
    public float period = 1f;
    private float theta;
    public float distance;

    public AudioSource movementAudioSource;
    public AudioSource screamAudioSource;


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
        enemyState = EnemyState.SPAWNING;
        attackState = AttackState.MOVING_TOWARDS_PLAYER;

        currentBanishTime = BANISH_TIME;

        Spawn();
    }


    void Update()
    {
        if (enemyState == EnemyState.SPAWNING)
        {
            return;
        }

        if (startedBanishing)
        {
            if (movementAudioSource.isPlaying)
                movementAudioSource.Stop();
            return;
        }

        if (healthSystem.IsDead())
        {
            Die();
        }


        if (enemyState == EnemyState.AGGRO)
        {
            if (attackState == AttackState.MOVING_TOWARDS_PLAYER)
            {
                if (!movementAudioSource.isPlaying)
                {
                    movementAudioSource.pitch = Random.Range(0.8f, 1.3f);
                    movementAudioSource.Play();

                }

                if (currentSpeed < MAX_SPEED)
                {
                    currentSpeed += ACCELERATION;
                }
                else
                {
                    currentSpeed = MAX_SPEED;
                }

                // Sinusoidal movement
                theta = Time.timeSinceLevelLoad / period;
                distance = amplitude * Mathf.Sin(theta);


                // Change to CHARGE
                if (Vector2.Distance(transform.position, player.transform.position) <= distanceToCharge)
                {
                    UpdatePlayerPosition();
                    directionOnChargeStart = (playerPosition - rigidbody.position).normalized;
                    attackState = AttackState.CHARGING; // Change state

                    transform.DOPunchRotation(new Vector3(0, 0, 20), CHARGE_TIME);
                }
            }
            else if (attackState == AttackState.CHARGING)
            {
                if (!screamAudioSource.isPlaying)
                {
                    screamAudioSource.pitch = Random.Range(0.8f, 1.3f);
                    screamAudioSource.Play();
                }

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
                movementAudioSource.Stop();

                spriteRenderer.color = new Color(0.36f, 0.36f, 0.36f, 1f);
                currentSpeed = 0f;
                Recovering();
            }
        }
    }



    private void FixedUpdate()
    {
        if (enemyState == EnemyState.SPAWNING)
        {
            return;
        }

        if (startedBanishing)
        {
            if (enemyState == EnemyState.SCARED)
            {
                FleeAwayFromPlayer();
            }
            return;
        }

        if (attackState == AttackState.MOVING_TOWARDS_PLAYER)
        {
            MoveTowardsPlayer();
        }
        else if (attackState == AttackState.CHARGING)
        {
            if (collidedWithPlayer)
            {
                PushPlayer();
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
            DealDamageToPlayer();
            collidedWithPlayer = true;
        }
    }

    private void MoveTowardsPlayer()
    {
        UpdatePlayerPosition();
        UpdateDirectionTowardsPlayerPosition();

        
        rigidbody.MovePosition((Vector2)transform.position + (Vector2.up * distance) + directionTowardsPlayerPosition * (currentSpeed * Time.deltaTime));
    }

    private void FleeAwayFromPlayer()
    {
        UpdatePlayerPosition();
        UpdateDirectionTowardsPlayerPosition();


        rigidbody.MovePosition((Vector2)transform.position + (Vector2.up * distance) + (-1 * directionTowardsPlayerPosition) * (currentSpeed * Time.deltaTime));
    }

    private void Charge()
    {
        rigidbody.MovePosition((Vector2)transform.position + directionOnChargeStart * (CHARGE_SPEED * Time.deltaTime));

        currentChargeTime -= Time.deltaTime;
        if (currentChargeTime <= 0)
        {
            currentChargeTime = CHARGE_TIME; // Reset value
            attackState = AttackState.RECOVERING; // Change state
            ResetColor();
        }
    }




    private void PushPlayer()
    {   
        player.GetComponent<Rigidbody2D>().AddForce(directionOnChargeStart * attackForce, ForceMode2D.Impulse);
    }

    private void Recovering()
    {
        if (currentAttackRecoverTime == ATTACK_RECOVER_TIME)
        {
            transform.DOPunchScale(new Vector3(-0.1f, -0.1f, 0), ATTACK_RECOVER_TIME, 0);
        }

        currentAttackRecoverTime -= Time.deltaTime;
        rigidbody.isKinematic = true;


        if (currentAttackRecoverTime <= 0)
        {
            hasRecovered = true;

            rigidbody.isKinematic = false;
            currentAttackRecoverTime = ATTACK_RECOVER_TIME; // Reset value
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // Reset color
            attackState = AttackState.MOVING_TOWARDS_PLAYER; // Change state
        }
    }

    public override void FleeAndBanish()
    {
        if (attackState == AttackState.RECOVERING)
        {
            rigidbody.isKinematic = false;
        }

        enemyState = EnemyState.SCARED;
        attackState = AttackState.MOVING_TOWARDS_PLAYER;
        Banish();
    }


}
