using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggroState : EnemyState
{
    SinMovement sinMovement;

    float moveSpeed;

    float distanceCloseToPlayerToCharge;

    bool isTouchingLight;


    private void Awake()
    {
        sinMovement = GetComponent<SinMovement>();
        moveSpeed = 8.0f;
        distanceCloseToPlayerToCharge = 4.0f;
    }


    protected override void StateDoStart()
    {
        isTouchingLight = false;
    }

    public override bool StateUpdate()
    {
        if (IsCloseToPlayerPosition())
        {
            nextState = EnemyStates.CHARGING;
            return true;
        }
        else if (isTouchingLight)
        {
            nextState = EnemyStates.SCARED;
            return true;
        }

        return false;
    }

    public override void StateFixedUpdate()
    {
        sinMovement.MoveTowardsTargetPosition(playerGameObject.transform.position, moveSpeed);
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
        return Vector2.Distance(playerGameObject.transform.position, transform.position) <= distanceCloseToPlayerToCharge;
    }

}
