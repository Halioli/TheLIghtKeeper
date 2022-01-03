using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuEnemy : HostileEnemy
{
    private Vector2 targetArea = new Vector2(10, 10);
    private Vector2 targetPosition;

    private float currentSpeed;
    private float minSpeed = 4f;
    private float maxSpeed = 8f;
    private float speedVariationInterval = 2f;
    private float waitTime = 0.5f;
    private bool doSpeedVariation = true;


    // Sinusoidal movement
    public float amplitude = 0.1f;
    private float period;
    private float theta;
    public float sinWaveDistance;


    private void Awake()
    {
        attackSystem = GetComponent<AttackSystem>();
        healthSystem = GetComponent<HealthSystem>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //player = GameObject.FindGameObjectWithTag("Player");
        player = null;
        collider = GetComponent<CapsuleCollider2D>();

        enemyState = EnemyState.SPAWNING;
        attackState = AttackState.MOVING_TOWARDS_PLAYER;

        currentBanishTime = BANISH_TIME;

        period = Random.Range(0.10f, 0.15f);
        collider = GetComponent<CapsuleCollider2D>();


        Spawn();
        StartCoroutine(SpeedVariation());
    }


    private void Update()
    {
        if (enemyState == EnemyState.WANDERING)
        {
            // Sinusoidal movement
            theta = Time.timeSinceLevelLoad / period;
            sinWaveDistance = amplitude * Mathf.Sin(theta);

            if (transform.position.Equals(targetPosition))
            {
                Disappear();
            }

            if (collider.IsTouchingLayers(LayerMask.NameToLayer("Light")))
            {
                FleeAndBanish();
            }

            if (doSpeedVariation)
            {
                StartCoroutine(SpeedVariation());
            }
        }
    }

    private void FixedUpdate()
    {
        if (enemyState == EnemyState.WANDERING)
        {
            MoveTowardsTarget();
        }
    }


    private void MoveTowardsTarget()
    {
        directionTowardsPlayerPosition = targetPosition - (Vector2)transform.position;
        rigidbody.MovePosition((Vector2)transform.position + (Vector2.up * sinWaveDistance) + directionTowardsPlayerPosition * (currentSpeed * Time.deltaTime));
    }


    IEnumerator SpeedVariation()
    {
        doSpeedVariation = false;
        currentSpeed = 0f;
        yield return new WaitForSeconds(waitTime);

        currentSpeed = Random.Range(minSpeed, maxSpeed);
        targetPosition = Random.insideUnitCircle * targetArea;
        yield return new WaitForSeconds(speedVariationInterval);
        doSpeedVariation = true;
    }
}
