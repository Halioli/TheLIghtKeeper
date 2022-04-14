using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeckoEnemy : EnemyMonster
{
    protected bool isDyingAlready;
    private Animator geckoAnimator;


    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        attackSystem = GetComponent<AttackSystem>();
        enemyAudio = GetComponent<EnemyAudio>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();

        geckoAnimator = GetComponent<Animator>();
    }


    private void Start()
    {
        InitColors();
    }


    void Update()
    {
        if (healthSystem.IsDead() && !isDyingAlready)
        {
            OnDeathStart();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerGameObject = other.gameObject;
            DealDamageToPlayer();
            PushPlayer();
        }
    }



    protected override void OnDeathStart()
    {
        isDyingAlready = true;

        geckoAnimator.SetBool("death", true);

        enemyAudio.StopFootstepsAudio();
        enemyAudio.PlayDeathAudio();
        enemyAudio.PlayScaredAudio();
    }

    protected override void OnDeathEnd()
    {
        DropItem(false);
        Destroy(gameObject);
    }



    public override void ReceiveDamage(int damageValue)
    {
        if (healthSystem.IsDead()) return;

        healthSystem.ReceiveDamage(damageValue);
        enemyAudio.PlayReceiveDamageAudio();

        StartCoroutine(HurtedFlashEffect());
    }


}
