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
    protected bool startedBanishing = false;

    protected const float BANISH_TIME = 1f;
    protected float currentBanishTime;



    // Public Attributes
    public ItemGameObject dropOnDeathItem;

    public AudioSource banishAudioSource;


    // Methods

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("CoreLight"))
        {
            startedBanishing = true;
        }
    }


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

    public void Banish()
    {
        startedBanishing = true;
        StartCoroutine("StartBanishing");
    }

    IEnumerator StartBanishing()
    {
        banishAudioSource.Play();

        Color fadeColor = spriteRenderer.material.color;
        fadeColor.a = 0.5f;

        spriteRenderer.material.color = fadeColor;

        while (currentBanishTime > 0f)
        {
            currentBanishTime -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Destroy(gameObject);
    }

}
