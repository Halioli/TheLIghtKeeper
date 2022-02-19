using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSpider : EnemyMonster
{
    ShadowSpiderStateManager shadowSpiderStateManager;

    bool isDyingAlready;


    void Awake()
    {
        attackSystem = GetComponent<AttackSystem>();
        healthSystem = GetComponent<HealthSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        shadowSpiderStateManager = GetComponent<ShadowSpiderStateManager>();
        enemyAudio = GetComponent<EnemyAudio>();

        isDyingAlready = false;
    }

    private void Start()
    {
        InitColors();
        InitShadowSpiderStateManager();
    }


    void Update()
    {
        if (healthSystem.IsDead() && !isDyingAlready)
        {
            OnDeathStart();
        }
    }


    private void FixedUpdate()
    {
        if (isGettingPushed) PushSelf();
    }



    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            AttackPlayer();
        }
        //else if (otherCollider.CompareTag("Light") || otherCollider.CompareTag("LampLight"))
        //{
        //    ReceiveDamage(10);
        //}
    }


    protected override void OnPlayerNotInLight()
    {
        shadowSpiderStateManager.ForceState(EnemyStates.WANDERING);
    }

    protected override void OnDeathStart()
    {
        isDyingAlready = true;
        shadowSpiderStateManager.ForceState(EnemyStates.DEATH);
    }

    public void SetPlayerGameObject(GameObject playerGameObject)
    {
        this.playerGameObject = playerGameObject;
    }

    private void InitShadowSpiderStateManager()
    {
        shadowSpiderStateManager.Init(playerGameObject.transform);
    }


    private void AttackPlayer()
    {
        DealDamageToPlayer();
        PushPlayer();
    }


    private void PushPlayer()
    {
        Vector2 pushDiretion = playerGameObject.transform.position - transform.position;
        playerGameObject.GetComponent<PlayerMovement>().GetsPushed(pushDiretion.normalized, attackSystem.pushValue);
    }


    public override void ReceiveDamage(int damageValue)
    {
        base.ReceiveDamage(damageValue);

        if (shadowSpiderStateManager.currentState == EnemyStates.SCARED)
            GetComponent<EnemyScaredState>().ResetFadeAnimation();
    }

}
