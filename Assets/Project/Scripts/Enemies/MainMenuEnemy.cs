using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuEnemy : MonoBehaviour
{
    private enum EnemyState
    {
        SPAWNING,
        WANDERING
    }
    private EnemyState enemyState;

    //Target
    private const float MIN_X_TARGET = -20f;
    private const float MAX_X_TARGET = 20f;
    private const float MIN_Y_TARGET = -20f;
    private const float MAX_Y_TARGET = -30f;
    //Spawn
    private const float MIN_X_SPAWN = -20f;
    private const float MAX_X_SPAWN = 20f;
    private const float MIN_Y_SPAWN = 20f;
    private const float MAX_Y_SPAWN = 30f;
    //Other
    private const float BANISH_TIME = 0.5f;
    private const float SPAWN_TIME = 0.5f;

    private Vector2 targetArea = new Vector2(10, 10);
    private Vector2 targetPosition;

    private float currentSpeed;
    private float minSpeed = 4f;
    private float maxSpeed = 8f;
    private float speedVariationInterval = 2f;
    private float waitTime = 0.5f;
    private bool doSpeedVariation = true;

    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D collider;
    private float currentBanishTime;
    private float currentSpawnTime;
    private Vector2 directionTowardsTaergetPosition;

    // Sinusoidal movement
    public float amplitude = 0.1f;
    private float period;
    private float theta;
    public float sinWaveDistance;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<CapsuleCollider2D>();

        enemyState = EnemyState.SPAWNING;

        currentBanishTime = BANISH_TIME;
        currentSpawnTime = SPAWN_TIME;

        period = Random.Range(0.10f, 0.15f);
        collider = GetComponent<CapsuleCollider2D>();

        RandomRespawn();
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
                RandomRespawn();
            }

            if (collider.IsTouchingLayers(LayerMask.NameToLayer("Light")))
            {
                Die();
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
        directionTowardsTaergetPosition = targetPosition - (Vector2)transform.position;
        rigidbody.MovePosition((Vector2)transform.position + (Vector2.up * sinWaveDistance) + directionTowardsTaergetPosition * (currentSpeed * Time.deltaTime));
    }

    private void SetRandomTargetPosition()
    {
        targetPosition = new Vector2(Random.Range(MIN_X_TARGET, MAX_X_TARGET), Random.Range(MAX_Y_TARGET, MIN_Y_TARGET));
    }

    private void RandomRespawn()
    {
        transform.position = new Vector2(Random.Range(MIN_X_SPAWN, MAX_X_SPAWN), Random.Range(MIN_Y_SPAWN, MAX_Y_SPAWN));
        ResetValues();

        StartCoroutine(Spawning());
        SetRandomTargetPosition();
    }

    private void Die()
    {
        Debug.Log("AAAAAAAAAAAAAAAAA");
    }

    private void ResetValues()
    {
        currentBanishTime = BANISH_TIME;
        currentSpawnTime = SPAWN_TIME;
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

    IEnumerator Spawning()
    {
        enemyState = EnemyState.SPAWNING;

        Color fadeColor = spriteRenderer.material.color;

        while (currentSpawnTime <= SPAWN_TIME)
        {
            fadeColor.a = currentSpawnTime / SPAWN_TIME;
            spriteRenderer.material.color = fadeColor;

            currentSpawnTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        enemyState = EnemyState.WANDERING;
    }
}
