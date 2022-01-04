using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuEnemy : MonoBehaviour
{
    private enum EnemyState
    {
        SPAWNING,
        WANDERING,
        WAITING
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
    private const float MAX_SPEED = 10f;

    private Animator animator;
    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private float currentBanishTime;
    private float currentSpawnTime;
    private float targetRadius = 0.1f;
    private Vector2 targetPosition;
    private Vector2 directionTarget;
    private Vector2 angleDirection;
    private bool enteredInLight = false;

    // Sinusoidal movement
    public float amplitude = 0.1f;
    private float period;
    private float theta;
    public float sinWaveDistance;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        enemyState = EnemyState.SPAWNING;

        currentBanishTime = BANISH_TIME;
        currentSpawnTime = SPAWN_TIME;

        period = Random.Range(0.10f, 0.15f);

        angleDirection = Vector2.zero;
        transform.position = Vector2.zero;

        RandomRespawn();
    }

    private void Update()
    {
        Debug.Log(targetPosition);
        if (enemyState == EnemyState.WANDERING)
        {
            if (Vector2.Distance(targetPosition, transform.position) < targetRadius)
            {
                RandomRespawn();
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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Light") && !enteredInLight)
        {
            enteredInLight = true;
            enemyState = EnemyState.WAITING;

            animator.SetTrigger("isDead");
            StartCoroutine(StartBanishing());
        }
    }

    private void MoveTowardsTarget()
    {
        directionTarget = (targetPosition - (Vector2)transform.position).normalized;

        // Sinusoidal movement
        theta = Time.timeSinceLevelLoad / period;
        sinWaveDistance = amplitude * Mathf.Sin(theta);

        angleDirection = Vector2.Perpendicular(directionTarget);
        angleDirection *= sinWaveDistance;

        rigidbody.MovePosition((Vector2)transform.position + angleDirection + directionTarget * (MAX_SPEED * Time.deltaTime));
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

    private void ResetValues()
    {
        enteredInLight = false;
        currentBanishTime = BANISH_TIME;
        currentSpawnTime = SPAWN_TIME;
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

    IEnumerator StartBanishing()
    {
        // Play banish audio sound
        //audioSource.clip = banishAudioClip;
        //audioSource.volume = Random.Range(0.1f, 0.2f);
        //audioSource.pitch = Random.Range(0.7f, 1.5f);
        //audioSource.Play();

        // Fading
        Color fadeColor = spriteRenderer.material.color;
        currentBanishTime = BANISH_TIME;
        while (currentBanishTime > 0f)
        {
            fadeColor.a = currentBanishTime / BANISH_TIME;
            spriteRenderer.material.color = fadeColor;

            currentBanishTime -= Time.deltaTime;
            yield return null;
        }

        RandomRespawn();
    }
}
