using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggroState : EnemyState
{
    SinMovement sinMovement;

    [SerializeField] float moveSpeed;

    [SerializeField] float distanceToCharge;
    [SerializeField] float chargeCooldownTime;

    bool isChargeCooldownStarted;
    bool isReadyToCharge;
    bool isTouchingLight;


    private void Awake()
    {
        sinMovement = GetComponent<SinMovement>();
        //moveSpeed = 12.0f;
        //distanceCloseToPlayerToCharge = 6.0f;
    }


    protected override void StateDoStart()
    {
        isChargeCooldownStarted = false;
        isReadyToCharge = false;
        isTouchingLight = false;
    }

    public override bool StateUpdate()
    {
        if (isTouchingLight)
        {
            nextState = EnemyStates.LIGHT_ENTER;
            return true;
        }
        else if (!isReadyToCharge && !isChargeCooldownStarted && IsCloseToPlayerPosition())
        {
            StartCoroutine(ChargeStartCooldown());
        }
        else if (!isReadyToCharge && isChargeCooldownStarted && !IsCloseToPlayerPosition())
        {
            StopCoroutine(ChargeStartCooldown());
            isChargeCooldownStarted = false;
            isReadyToCharge = false;
        }
        else if (isReadyToCharge)
        {
            nextState = EnemyStates.CHARGING;
            return true;
        }

        return false;
    }

    public override void StateFixedUpdate()
    {
        if (!isReadyToCharge) sinMovement.MoveTowardsTargetPosition(playerTransform.position, moveSpeed);
    }

    public override void StateOnTriggerEnter(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.layer == LayerMask.NameToLayer("Light"))
        {
            isTouchingLight = true;
        }
    }



    private bool IsCloseToPlayerPosition()
    {
        return Vector2.Distance(playerTransform.position, transform.position) <= distanceToCharge;
    }

    IEnumerator ChargeStartCooldown()
    {
        isChargeCooldownStarted = true;
        isReadyToCharge = false;

        yield return new WaitForSeconds(chargeCooldownTime);

        isChargeCooldownStarted = false;
        isReadyToCharge = true;
    }


}
