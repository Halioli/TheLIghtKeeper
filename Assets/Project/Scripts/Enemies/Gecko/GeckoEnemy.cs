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


    protected override void OnDeathStart()
    {
        isDyingAlready = true;

        geckoAnimator.SetBool("death", true);
        enemyAudio.PlayDeathAudio();
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
