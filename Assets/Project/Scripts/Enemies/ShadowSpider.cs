using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSpider : MonoBehaviour
{
    [SerializeField] GameObject playerGameObject;

    AttackSystem attackSystem;
    HealthSystem healthSystem;
    ShadowSpiderStateManager shadowSpiderStateManager;

    bool isDyingAlready;


    void Awake()
    {
        attackSystem = GetComponent<AttackSystem>();
        healthSystem = GetComponent<HealthSystem>();
        shadowSpiderStateManager = GetComponent<ShadowSpiderStateManager>();

        isDyingAlready = false;

        InitShadowSpiderStateManager();
    }


    void Update()
    {
        if (healthSystem.IsDead() && !isDyingAlready)
        {
            isDyingAlready = true;
            shadowSpiderStateManager.ForceDeathState();
        }
    }


    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Player"))
        {
            AttackPlayer();
        }
    }



    public void SetPlayerGameObject(GameObject playerGameObject)
    {
        this.playerGameObject = playerGameObject;
    }

    private void InitShadowSpiderStateManager()
    {
        shadowSpiderStateManager.Init();
    }


    private void AttackPlayer()
    {
        DealDamageToPlayer();
        PushPlayer();
    }

    protected void DealDamageToPlayer()
    {
        playerGameObject.GetComponent<PlayerCombat>().ReceiveDamage(attackSystem.attackValue);
    }

    private void PushPlayer()
    {
        Vector2 pushDiretion = playerGameObject.transform.position - transform.position;
        playerGameObject.GetComponent<PlayerMovement>().GetsPushed(pushDiretion.normalized, attackSystem.pushValue);
    }


}
