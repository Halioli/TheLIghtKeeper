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



    // Public Attributes
    public ItemGameObject dropOnDeathItem;

    public AudioSource audioSource;
    public AudioClip banishAudioClip;
    public AudioClip hurtedAudioClip;


    // Events
    public delegate void EnemyDisappears();
    public static event EnemyDisappears enemyDisappearsEvent;


    // Methods

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Light"))
        {
            FleeAndBanish();
        }
    }



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



    protected void Die()
    {
        // Play death animation
        DropItem();
        Banish();
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

        enemyDisappearsEvent();
    }

    IEnumerator StartBanishing()
    {
        // Play banish audio sound
        audioSource.clip = banishAudioClip;
        audioSource.volume = Random.Range(0.1f, 0.3f);
        audioSource.pitch = Random.Range(0.7f, 1.5f);
        audioSource.Play();

        // Fading
        ResetColor();
        Color fadeColor = spriteRenderer.material.color;
        while (currentBanishTime > 0f)
        {
            fadeColor.a = currentBanishTime / BANISH_TIME;
            spriteRenderer.material.color = fadeColor;

            currentBanishTime -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Destroy(gameObject);
    }

    protected void ResetColor()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // Reset color
    }

    public virtual void FleeAndBanish()
    {       
        enemyState = EnemyState.SCARED;
        attackState = AttackState.MOVING_TOWARDS_PLAYER;
        Banish();
    }
}