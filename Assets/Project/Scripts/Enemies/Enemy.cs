using System.Collections;
using System.Collections.Generic;
using UnityEngine;


abstract public class Enemy : MonoBehaviour
{
    protected enum EnemyState
    {
        WANDERING,
        AGGRO
    }

    protected enum AttackState
    {
        MOVING_TOWARDS_PLAYER,
        CHARGING,
        RECOVERING
    }



    // Protected Attributes
    protected EnemyState enemyState;
    protected AttackState attackState;

    protected GameObject player;
    protected Vector2 playerPosition;
    protected Vector2 directionTowardsPlayerPosition;

    protected Rigidbody2D rigidbody;

    protected AttackSystem attackSystem;
    protected HealthSystem healthSystem;
    protected SpriteRenderer spriteRenderer;

    protected int damageToDeal;


    //private void Start()
    //{
    //    attackSystem = GetComponent<AttackSystem>();
    //    healthSystem = GetComponent<HealthSystem>();
    //    rigidbody = GetComponent<Rigidbody2D>();
    //    spriteRenderer = GetComponent<SpriteRenderer>();
    //    player = GameObject.FindGameObjectWithTag("Player");
    //}



    // Methods
    protected void UpdatePlayerPosition() { 
        playerPosition = player.transform.position;
    }

    protected void UpdateDirectionTowardsPlayerPosition()
    {
        directionTowardsPlayerPosition = (playerPosition - rigidbody.position).normalized;
    }

    protected void DamagePlayer()
    {
        attackSystem.DamageHealthSystemWithAttackValue(player.GetComponent<HealthSystem>());        
    }
}
