using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWanderingState : EnemyState
{
    EnemyAudio enemyAudio;
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
        enemyAudio = GetComponent<EnemyAudio>();
        sinMovement = GetComponent<SinMovement>();
    }


    protected override void StateDoStart()
    {
        isMoving = true;

        SetWanderingCentrePosition();
        StartCoroutine(WaitForNewWanderingTargetPosition());
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

            enemyAudio.PlayFootstepsAudio();
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
        enemyAudio.StopFootstepsAudio();

        yield return new WaitForSeconds(wanderingWaitTime);

        isMoving = true;
        enemyAudio.PlayFootstepsAudio();
        SetWanderingTargetPosition();
    }


    private bool IsCloseToPlayerPosition()
    {
        return Vector2.Distance(playerTransform.position, transform.position) <= distanceToAggro;
    }


}
