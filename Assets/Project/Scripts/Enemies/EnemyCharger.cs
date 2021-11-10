using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyState
{
    WANDERING,
    AGGRO
}

enum AttackState
{
    MOVING_TOWARDS_PLAYER,
    CHARGING,
    RECOVERING
}

public class EnemyCharger : MonoBehaviour
{
    // Public
    public const float ATTACK_RECOVER_TIME = 3f;
    public const float CHARGE_SPEED = 8f;
    public const float CHARGE_TIME = 1f;

    public GameObject player;
    public AttackSystem attackSystem;
    public Rigidbody2D rigidbody;
    public float maxSpeed = 5f;
    public float acceleration = 0.25f;
    public float attackForce = 10f;

    // Private
    private EnemyState enemyState;
    private AttackState attackState;

    private Vector2 playerPosition;
    private Vector2 directionTowardsPlayerPosition;
    private Vector2 directionOnChargeStart;

    private float currentSpeed;
    private float currentAttackRecoverTime;
    private bool hasRecovered;
    private bool collidedWithPlayer;
    private float currentChargeTime;

    private void Start()
    {
        currentAttackRecoverTime = ATTACK_RECOVER_TIME;
        rigidbody = GetComponent<Rigidbody2D>();
        currentSpeed = 1f;
        hasRecovered = false;
        collidedWithPlayer = false;
        enemyState = EnemyState.AGGRO;
        attackState = AttackState.MOVING_TOWARDS_PLAYER;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState == EnemyState.AGGRO)
        {
            if (attackState == AttackState.MOVING_TOWARDS_PLAYER)
            {
                MoveTowardsPlayer();

                // Change to CHARGE
                if (Vector2.Distance(transform.position, player.transform.position) <= 4f)
                {
                    //directionOnChargeStart = (player.transform.position - rigidbody.position).normalized;
                    attackState = AttackState.CHARGING; // Change state
                }
            }
            else if (attackState == AttackState.CHARGING)
            {
                if (collidedWithPlayer)
                {
                    collidedWithPlayer = false;
                    currentChargeTime = CHARGE_TIME; // Reset value
                    attackState = AttackState.RECOVERING; // Change state
                }
            }
            else if (attackState == AttackState.RECOVERING)
            {
                Recovering();
            }
        }

        if (currentSpeed < maxSpeed)
        {
            currentSpeed += acceleration;
        }
        else
        {
            currentSpeed = maxSpeed;
        }
    }

    private void FixedUpdate()
    {
        if (attackState == AttackState.CHARGING)
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
        Debug.Log(collision.collider);
        if (collision.collider.GetComponent<HealthSystem>() != null)
        {
            attackSystem.DamageHealthSystemWithAttackValue(collision.collider.GetComponent<HealthSystem>());

            collidedWithPlayer = true;
        }
    }

    private void PushPlayerAndSelf()
    {
        Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerRigidbody.AddForce(Vector2.up * attackForce, ForceMode2D.Impulse);
        rigidbody.AddForce(Vector2.right * attackForce, ForceMode2D.Impulse);
    }

    private void MoveTowardsPlayer()
    {
        playerPosition = player.transform.position;
        directionTowardsPlayerPosition = (playerPosition - rigidbody.position).normalized;

        rigidbody.MovePosition((Vector2)transform.position + directionTowardsPlayerPosition * (currentSpeed * Time.deltaTime));
    }

    private void Charge()
    {
        rigidbody.MovePosition((Vector2)transform.position + directionOnChargeStart * (CHARGE_SPEED * Time.deltaTime));

        currentChargeTime -= Time.deltaTime;
        if (currentChargeTime <= 0)
        {
            currentChargeTime = CHARGE_TIME; // Reset value
            attackState = AttackState.RECOVERING; // Change state
        }
    }

    private void Recovering()
    {
        currentAttackRecoverTime -= Time.deltaTime;
        if (currentAttackRecoverTime <= 0)
        {
            hasRecovered = true;

            currentAttackRecoverTime = ATTACK_RECOVER_TIME; // Reset value
            attackState = AttackState.MOVING_TOWARDS_PLAYER; // Change state
        }
    }
}
