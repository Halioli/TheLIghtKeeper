using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargingState : EnemyState
{
    SinMovement sinMovement;

    bool hasStartedCharging;
    bool isChargeDone;
    bool isExhaustDone;
    float moveSpeed;
    float chargeTime;
    float chargeExhaustTime;

    bool isTouchingLight;

    private void Awake()
    {
        sinMovement = GetComponent<SinMovement>();

        moveSpeed = 10.0f;
        chargeTime = 0.5f;
        chargeExhaustTime = 1.0f;
    }


    protected override void StateDoStart()
    {
        hasStartedCharging = false;
        isChargeDone = false;
        isExhaustDone = false;
        isTouchingLight = false;
    }

    public override bool StateUpdate()
    {
        if (!hasStartedCharging)
        {
            StartCoroutine(ChargeTimer());
        }
        else if (isExhaustDone)
        {
            nextState = EnemyStates.AGGRO;
            return true;
        }
        else if (isTouchingLight)
        {
            if (!isExhaustDone) StopCoroutine(ChargeTimer());
            nextState = EnemyStates.SCARED;
            return true;
        }

        return false;
    }

    public override void StateFixedUpdate()
    {
        if (!isChargeDone) sinMovement.MoveTowardsTargetPosition(playerGameObject.transform.position, moveSpeed);
    }

    public override void StateOnTriggerEnter(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.layer == LayerMask.NameToLayer("Light"))
        {
            isTouchingLight = true;
        }
    }


    IEnumerator ChargeTimer()
    {
        hasStartedCharging = true;
        yield return new WaitForSeconds(chargeTime);
        isChargeDone = true;
        yield return new WaitForSeconds(chargeExhaustTime);
        isExhaustDone = true;
    }

}
