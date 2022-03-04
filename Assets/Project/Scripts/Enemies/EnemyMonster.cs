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
    protected EnemyAudio enemyAudio;

    protected bool isGettingPushed = false;
    protected Vector2 pushedDirection;
    protected float pushedForce;

    protected float damagedTime = 0.2f;
    private Color normal;
    private Color transparent;


    [SerializeField] protected GameObject playerGameObject;

    [SerializeField] protected int dropRatePercent = 100;
    public ItemGameObject dropOnDeathItem;





    private void OnEnable()
    {
        DarknessSystem.OnPlayerNotInLight += OnPlayerNotInLight;
    }

    private void OnDisable()
    {
        DarknessSystem.OnPlayerNotInLight -= OnPlayerNotInLight;
    }

    protected void InitColors()
    {
        normal = spriteRenderer.color;
        transparent = spriteRenderer.color;
        transparent.a = 0.25f;
    }


    protected virtual void OnPlayerNotInLight()
    {
    }


    public virtual void ReceiveDamage(int damageValue)
    {
        if (healthSystem.IsDead()) return;

        healthSystem.ReceiveDamage(damageValue);
        enemyAudio.PlayReceiveDamageAudio();

        StartCoroutine(HurtedFlashEffect());
    }

    public virtual void SetPlayer(GameObject playerGameObject)
    {
        this.playerGameObject = playerGameObject;
    }

    protected virtual void DealDamageToPlayer()
    {
        playerGameObject.GetComponent<PlayerCombat>().ReceiveDamage(attackSystem.attackValue);
    }


    protected virtual void OnDeathStart()
    {
    }

    protected virtual void OnDeathEnd()
    {
        DropItem();
        Destroy(gameObject);
    }

    protected void DropItem()
    {
        if (Random.Range(0, 100) > dropRatePercent) return;

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
        transform.DOComplete();

        transform.DOPunchScale(new Vector3(-0.4f, -0.4f, 0), damagedTime);

        //spriteRenderer.color = transparent;
        yield return new WaitForSeconds(damagedTime / 2);

        spriteRenderer.color = normal;
        yield return new WaitForSeconds(damagedTime / 2);
        
        spriteRenderer.color = normal;
    }




}
