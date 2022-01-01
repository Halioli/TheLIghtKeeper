using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

abstract public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        SPAWNING,
        WANDERING,
        AGGRO,
        SCARED
    }

    public enum AttackState
    {
        MOVING_TOWARDS_PLAYER,
        CHARGING,
        RECOVERING
    }


    // Protected Attributes
    public EnemyState enemyState;
    public AttackState attackState;

    protected GameObject player;
    protected Vector2 playerPosition;
    protected Vector2 directionTowardsPlayerPosition;

    protected Rigidbody2D rigidbody;

    protected AttackSystem attackSystem;
    protected HealthSystem healthSystem;
    protected SpriteRenderer spriteRenderer;

    protected int damageToDeal;
    protected bool startedBanishing = false;

    public const float SPAWN_TIME = 0.5f;
    protected float currentSpawnTime = 0f;

    protected const float BANISH_TIME = 1f;
    protected float currentBanishTime;

    protected bool getsPushed = false;
    protected Vector2 pushedDirection = new Vector2();
    protected float pushedForce = 0f;


    // Public Attributes
    public ItemGameObject dropOnDeathItem;

    public AudioSource audioSource;
    public AudioClip banishAudioClip;
    public AudioClip hurtedAudioClip;


    // Methods

    public void Spawn()
    {
        StartCoroutine("Spawning");
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

        enemyState = EnemyState.AGGRO;
    }

    protected void UpdatePlayerPosition()
    {
        playerPosition = player.transform.position;
    }

    protected void UpdateDirectionTowardsPlayerPosition()
    {
        directionTowardsPlayerPosition = (playerPosition - rigidbody.position).normalized;
    }

    public void ReceiveDamage(int damageValue)
    {
        healthSystem.ReceiveDamage(damageValue);

        transform.DOPunchScale(new Vector3(-0.4f, -0.4f, 0), 0.15f);

        audioSource.clip = hurtedAudioClip;
        audioSource.pitch = Random.Range(0.8f, 1.3f);
        audioSource.Play();
    }

    protected void DealDamageToPlayer()
    {
        player.GetComponent<PlayerCombat>().ReceiveDamage(attackSystem.attackValue);
    }



    protected virtual void Die()
    {
        // Play death animation
        DropItem();
    }

    protected void DropItem()
    {
        ItemGameObject item = Instantiate(dropOnDeathItem, transform.position, Quaternion.identity);
        item.DropsDown();
    }


    protected void ResetColor()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // Reset color
    }

    public void GetsPushed(Vector2 direction, float force)
    {
        getsPushed = true;
        pushedDirection = direction;
        pushedForce = force;
    }

    protected void Pushed()
    {
        rigidbody.AddForce(pushedDirection * pushedForce, ForceMode2D.Impulse);
        getsPushed = false;
    }
}