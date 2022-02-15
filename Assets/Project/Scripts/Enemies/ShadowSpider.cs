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
        InitShadowSpiderStateManager();
    }


    void Update()
    {
        if (healthSystem.IsDead() && !isDyingAlready)
        {
            OnDeathStart();
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReceiveDamage(5);
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            shadowSpiderStateManager.ForceState(EnemyStates.SCARED);
        }
    }


    private void FixedUpdate()
    {
        if (isGettingPushed) PushSelf();
    }



    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Player"))
        {
            AttackPlayer();
        }
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
