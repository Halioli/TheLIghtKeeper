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

    public ItemGameObject dropOnDeathItem;



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

    protected void Die()
    {
        // Play death animation
        DropItem();
        Destroy(gameObject);
    }

    protected void DropItem()
    {
        ItemGameObject item = Instantiate(dropOnDeathItem, transform.position, Quaternion.identity);
        item.DropsDown();
    }
}
