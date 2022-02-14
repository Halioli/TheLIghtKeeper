using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class EnemyMonster : MonoBehaviour
{
    protected AttackSystem attackSystem;
    protected HealthSystem healthSystem;
    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rigidbody;

    protected bool isGettingPushed = false;
    protected Vector2 pushedDirection;
    protected float pushedForce;

    [SerializeField] protected GameObject playerGameObject;

    public ItemGameObject dropOnDeathItem;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip banishAudioClip;
    [SerializeField] AudioClip receiveDamageAudioClip;


    // Methods
    public virtual void ReceiveDamage(int damageValue)
    {
        if (healthSystem.IsDead()) return;

        healthSystem.ReceiveDamage(damageValue);

        transform.DOPunchScale(new Vector3(-0.4f, -0.4f, 0), 0.15f);

        audioSource.clip = receiveDamageAudioClip;
        audioSource.pitch = Random.Range(0.8f, 1.3f);
        audioSource.Play();

        StartCoroutine(HurtedFlashEffect());
    }

    protected virtual void DealDamageToPlayer()
    {
        playerGameObject.GetComponent<PlayerCombat>().ReceiveDamage(attackSystem.attackValue);
    }



    protected virtual void Die()
    {
        // Play death animation
        DropItem();
    }

    protected void DropItem()
    {
        ItemGameObject item = Instantiate(dropOnDeathItem, transform.position, Quaternion.identity);
        item.DropsRandom();
    }


    public void GetsPushed(Vector2 direction, float force)
    {
        isGettingPushed = true;
        pushedDirection = direction;
        pushedForce = force;
    }

    protected void PushSelf()
    {
        rigidbody.AddForce(pushedDirection * pushedForce, ForceMode2D.Impulse);
        isGettingPushed = false;
    }

    IEnumerator HurtedFlashEffect()
    {
        int count = 3;
        Color normal = spriteRenderer.color;
        Color transparent = spriteRenderer.color;
        transparent.a = 0.1f;


        while (--count > 0)
        {
            spriteRenderer.color = transparent;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = normal;
            yield return new WaitForSeconds(0.2f);
        }
        spriteRenderer.color = normal;
    }

}
