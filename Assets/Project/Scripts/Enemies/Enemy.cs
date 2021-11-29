using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    protected const float BANISH_TIME = 0.5f;
    protected float currentBanishTime;



    // Public Attributes
    public ItemGameObject dropOnDeathItem;

    public AudioSource audioSource;
    public AudioClip banishAudioClip;
    public AudioClip hurtedAudioClip;


    // Methods

    private void OnTriggerEnter2D(Collider2D collider)
    {      
        if (collider.gameObject.layer == LayerMask.NameToLayer("Light"))
        {
            Banish();
        }
    }


    protected void UpdatePlayerPosition() { 
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
        DealDamage(player.GetComponent<HealthSystem>());
    }

    public void DealDamage(HealthSystem healthSystemToDealDamage)
    {
        healthSystemToDealDamage.ReceiveDamage(attackSystem.attackValue);
        
        //attackAudioSource.Play();
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
        // Play banish audio sound
        audioSource.clip = banishAudioClip;
        audioSource.pitch = Random.Range(0.8f, 1.3f);
        audioSource.Play();

        // Fading
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
