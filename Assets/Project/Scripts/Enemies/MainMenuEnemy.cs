using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    private const float BANISH_TIME = 1.07f;
    private const float SPAWN_TIME = 0.5f;
    private const float MAX_SPEED = 20f;

    private AudioSource movementAudioSource;
    private AudioSource screamAudioSource;
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

    //Sinusoidal movement
    public float amplitude = 0.1f;
    private float period;
    private float theta;
    public float sinWaveDistance;

    // Public Attributes
    public AudioClip deathAudioClip;
    public AudioClip movementAudioClip;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        movementAudioSource = GetComponents<AudioSource>()[0];
        screamAudioSource = GetComponents<AudioSource>()[1];

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
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        currentBanishTime = BANISH_TIME;
        currentSpawnTime = SPAWN_TIME;
    }

    private void StartDying()
    {
        StartCoroutine(StartBanishing());
    }

    private void Die()
    {
        RandomRespawn();
    }

    IEnumerator Spawning()
    {
        //Play move audio sound
        screamAudioSource.Stop();
        movementAudioSource.clip = movementAudioClip;
        movementAudioSource.volume = Random.Range(0.1f, 0.2f);
        movementAudioSource.pitch = Random.Range(0.7f, 1.5f);
        movementAudioSource.Play();

        enemyState = EnemyState.SPAWNING;

        Color fadeColor = spriteRenderer.color;

        while (currentSpawnTime <= SPAWN_TIME)
        {
            fadeColor.a = currentSpawnTime / SPAWN_TIME;
            spriteRenderer.color = fadeColor;

            currentSpawnTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        enemyState = EnemyState.WANDERING;
    }

    IEnumerator StartBanishing()
    {
        //Play death audio sound
        movementAudioSource.Stop();
        screamAudioSource.clip = deathAudioClip;
        screamAudioSource.volume = Random.Range(0.1f, 0.2f);
        screamAudioSource.pitch = Random.Range(0.7f, 1.5f);
        screamAudioSource.Play();

        // Fading
        Color fadeColor = spriteRenderer.color;
        currentBanishTime = BANISH_TIME;

        while (currentBanishTime > 0f)
        {
            fadeColor.a = currentBanishTime / BANISH_TIME;
            spriteRenderer.color = fadeColor;

            currentBanishTime -= Time.deltaTime;
            yield return null;
        }
    }
}
