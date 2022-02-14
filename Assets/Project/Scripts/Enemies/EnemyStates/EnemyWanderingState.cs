using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWanderingState : EnemyState
{
    SinMovement sinMovement;

    bool isMoving;
    [SerializeField] float minimumWanderDistance;
    [SerializeField] float wanderingWaitTime;
    [SerializeField] float wanderingRadius;
    [SerializeField] float moveSpeed;

    Vector2 wanderingCentrePosition;
    Vector2 targetPosition;

    [SerializeField] float distanceToAggro;


    private void Awake()
    {
        sinMovement = GetComponent<SinMovement>();

        //wanderingRadius = 7.0f;
        //wanderingWaitTime = 3.0f;
        //moveSpeed = 5.0f;
        //distanceCloseToPlayerToAggro = 10.0f;
    }


    protected override void StateDoStart()
    {
        isMoving = true;

        SetWanderingCentrePosition();
        SetWanderingTargetPosition();
    }

    public override bool StateUpdate()
    {
        if (isMoving && sinMovement.IsNearTargetPosition(targetPosition))
        {
            StartCoroutine(WaitForNewWanderingTargetPosition());
        }

        if (IsCloseToPlayerPosition())
        {
            if (!isMoving) StopCoroutine(WaitForNewWanderingTargetPosition());

            nextState = EnemyStates.AGGRO;
            return true;
        }


        return false;
    }

    public override void StateFixedUpdate()
    {
        if (!isMoving) return;
        
        sinMovement.MoveTowardsTargetPosition(targetPosition, moveSpeed);
    }



    private void SetWanderingCentrePosition()
    {
        wanderingCentrePosition = transform.position;
    }
    private void SetWanderingTargetPosition()
    {
        do {
            targetPosition = wanderingCentrePosition + Random.insideUnitCircle * wanderingRadius;
        } while (Vector2.Distance(targetPosition, transform.position) < minimumWanderDistance);
    }


    IEnumerator WaitForNewWanderingTargetPosition()
    {
        isMoving = false;

        yield return new WaitForSeconds(wanderingWaitTime);

        isMoving = true;
        SetWanderingTargetPosition();
    }


    private bool IsCloseToPlayerPosition()
    {
        return Vector2.Distance(playerTransform.position, transform.position) <= distanceToAggro;
    }


}
