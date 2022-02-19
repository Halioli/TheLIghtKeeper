using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargingState : EnemyState
{
    EnemyAudio enemyAudio;
    SinMovement sinMovement;

    bool hasStartedCharging;
    bool isChargeDone;
    bool isExhaustDone;
    [SerializeField] float moveSpeed;
    [SerializeField] float chargeTime;
    [SerializeField] float chargeExhaustTime;
    Vector2 chargeTargetDirection;

    bool isTouchingLight;

    private void Awake()
    {
        enemyAudio = GetComponent<EnemyAudio>();
        sinMovement = GetComponent<SinMovement>();

        //moveSpeed = 30.0f;
        //chargeTime = 0.5f;
        //chargeExhaustTime = 1.0f;
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
        if (isTouchingLight)
        {
            if (!isExhaustDone) StopCoroutine(ChargeTimer());
            nextState = EnemyStates.LIGHT_ENTER;
            return true;
        }
        else if (!hasStartedCharging)
        {
            StartCoroutine(ChargeTimer());
        }
        else if (isExhaustDone)
        {
            nextState = EnemyStates.AGGRO;
            return true;
        }

        return false;
    }

    public override void StateFixedUpdate()
    {
        if (!isChargeDone) sinMovement.MoveTowardsTargetDirectionStraight(chargeTargetDirection, moveSpeed);
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
        chargeTargetDirection = (playerTransform.position - transform.position).normalized;
        enemyAudio.PlayAttackAudio();
        enemyAudio.PlayDashAudio();

        hasStartedCharging = true;
        isChargeDone = false;
        yield return new WaitForSeconds(chargeTime);

        isChargeDone = true;
        enemyAudio.StopFootstepsAudio();
        yield return new WaitForSeconds(chargeExhaustTime);

        isExhaustDone = true;
        enemyAudio.PlayFootstepsAudio();
    }

}
