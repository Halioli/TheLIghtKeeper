using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Audio;


public class EnemyCharger : HostileEnemy
{
    // Private Attributes
    private Animator animator;
    bool died = false;

    private Vector2 directionOnChargeStart;
    private Vector2 angleDirection;
    private Vector2 fleeDirection;

    private float currentSpeed;
    private float currentAttackRecoverTime;
    private float currentChargeTime;
    private bool hasRecovered;
    private bool collidedWithPlayer;
    private AudioSource enemyAudioSource;

    // Public Attributes
    public const float ATTACK_RECOVER_TIME = 2f;
    public float CHARGE_SPEED;
    public const float CHARGE_TIME = 0.5f;
    public float MAX_SPEED;
    public const float ACCELERATION = 0.25f;

    public float distanceToCharge = 4f;
    public AudioMixerSnapshot[] snapshots;

    // Sinusoidal movement
    public float amplitude = 0.1f;
    private float period;
    private float theta;
    public float sinWaveDistance;

    public AudioSource movementAudioSource;
    public AudioSource screamAudioSource;

    private void Start()
    {
        animator = GetComponent<Animator>();

        attackSystem = GetComponent<AttackSystem>();
        healthSystem = GetComponent<HealthSystem>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        collider = GetComponent<CapsuleCollider2D>();

        currentAttackRecoverTime = ATTACK_RECOVER_TIME;
        currentChargeTime = CHARGE_TIME;
        currentSpeed = 1f;
        hasRecovered = false;
        collidedWithPlayer = false;
        enemyState = EnemyState.SPAWNING;
        attackState = AttackState.MOVING_TOWARDS_PLAYER;

        currentBanishTime = BANISH_TIME;

        period = Random.Range(0.10f, 0.15f);

        Spawn();
    }


    void Update()
    {
        if (enemyState == EnemyState.SPAWNING)
        {
            return;
        }

        if (startedBanishing || PauseMenu.gameIsPaused)
        {
            if (movementAudioSource.isPlaying)
                movementAudioSource.Stop();
            return;
        }

        if (healthSystem.IsDead() && !died)
        {
            animator.SetTrigger("isDead");
            died = true;
        }

        if (enemyState == EnemyState.SCARED || enemyState == EnemyState.WANDERING || died)
        {
            return;
        }
        else if (enemyState == EnemyState.AGGRO)
        {
            if (attackState == AttackState.MOVING_TOWARDS_PLAYER)
            {
                if (!movementAudioSource.isPlaying)
                {
                    movementAudioSource.pitch = Random.Range(0.8f, 1.3f);
                    movementAudioSource.Play();
                }
                else if (PauseMenu.gameIsPaused)
                {
                    movementAudioSource.Stop();
                }

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
            }
            else if (attackState == AttackState.RECOVERING)
            {
                movementAudioSource.Stop();
            }
        }


        if (collider.IsTouchingLayers(LayerMask.NameToLayer("Light")))
        {
            FleeAndBanish();
        }

    }



    private void FixedUpdate()
    {
        if (getsPushed)
        {
            Pushed();
            return;
        }

        if (enemyState == EnemyState.SPAWNING || enemyState == EnemyState.WANDERING || died)
        {
            return;
        }
        else if (enemyState == EnemyState.SCARED)
        {
            FleeAway();
            return;
        }

        if (attackState == AttackState.MOVING_TOWARDS_PLAYER)
        {
            MoveTowardsPlayer();
            fleeDirection = directionTowardsPlayerPosition;
        }
        else if (attackState == AttackState.CHARGING)
        {
            Charge();
            fleeDirection = directionOnChargeStart;
            StartCoroutine(StartRecovering());
        }
    }



    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") && !died)
        {
            DealDamageToPlayer();
            PushPlayer();
        }
        else if (collider.gameObject.layer == LayerMask.NameToLayer("Light"))
        {
            // Play banish audio sound
            audioSource.clip = banishAudioClip;
            audioSource.volume = Random.Range(0.1f, 0.2f);
            audioSource.pitch = Random.Range(0.7f, 1.5f);
            audioSource.Play();
            FleeAndBanish();
        }
    }

    private void MoveTowardsPlayer()
    {
        UpdatePlayerPosition();
        UpdateDirectionTowardsPlayerPosition();

        // Sinusoidal movement
        theta = Time.timeSinceLevelLoad / period;
        sinWaveDistance = amplitude * Mathf.Sin(theta);

        angleDirection = Vector2.Perpendicular(directionTowardsPlayerPosition);
        angleDirection *= sinWaveDistance;

        rigidbody.MovePosition((Vector2)transform.position + angleDirection + directionTowardsPlayerPosition * (currentSpeed * Time.deltaTime));
    }

    private void FleeAway()
    {
        UpdatePlayerPosition();
        UpdateDirectionTowardsPlayerPosition();

        // Sinusoidal movement
        theta = Time.timeSinceLevelLoad / period;
        sinWaveDistance = amplitude * Mathf.Sin(theta);

        angleDirection = Vector2.Perpendicular(directionOnChargeStart);
        angleDirection *= sinWaveDistance;

        rigidbody.MovePosition((Vector2)transform.position - angleDirection - fleeDirection * (MAX_SPEED * Time.deltaTime));
    }

    private void Charge()
    {
        rigidbody.AddForce(directionOnChargeStart.normalized * CHARGE_SPEED, ForceMode2D.Impulse);
        attackState = AttackState.RECOVERING; // Change state
    }


    private void PushPlayer()
    {
        player.GetComponent<PlayerMovement>().GetsPushed(directionOnChargeStart, attackSystem.pushValue);
    }

    IEnumerator StartRecovering()
    {
        transform.DOPunchScale(new Vector3(-0.1f, -0.1f, 0), ATTACK_RECOVER_TIME, 0);
        yield return new WaitForSeconds(ATTACK_RECOVER_TIME);
        attackState = AttackState.MOVING_TOWARDS_PLAYER;
    }



    protected override void FleeAndBanish()
    {
        enemyState = EnemyState.SCARED;
        attackState = AttackState.MOVING_TOWARDS_PLAYER;
        Banish();
    }


    private void StartDying()
    {
        //Play death audio sound
        transform.DOShakePosition(2, 0.5f);

        audioSource.Stop();
        audioSource.clip = deathAudioClip;
        audioSource.volume = Random.Range(0.1f, 0.2f);
        audioSource.pitch = Random.Range(0.7f, 1.5f);
        audioSource.Play();
    }

}