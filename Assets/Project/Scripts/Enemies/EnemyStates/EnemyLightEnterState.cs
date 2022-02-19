using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLightEnterState : EnemyState
{
    EnemyAudio enemyAudio;
    SinMovement sinMovement;

    [SerializeField] float moveSpeedTragetingPlayer;
    [SerializeField] float timeTargetingPlayer;
    bool isTargetingPlayer;
    [SerializeField] float moveSpeedScapingPlayer;
    [SerializeField] float timeBeforeScared;
    bool isWaitFinished;
    float moveSpeed;



    private void Awake()
    {
        enemyAudio = GetComponent<EnemyAudio>();
        sinMovement = GetComponent<SinMovement>();
    }

    protected override void StateDoStart()
    {
        isTargetingPlayer = true;
        isWaitFinished = false;
        moveSpeed = moveSpeedTragetingPlayer;
        StartCoroutine(WaitBeforeScared());
    }

    public override bool StateUpdate()
    {
        if (isWaitFinished)
        {
            nextState = EnemyStates.SCARED;
            return true;
        }

        return false;
    }

    public override void StateFixedUpdate()
    {
        if (isTargetingPlayer)
            sinMovement.MoveTowardsTargetPosition(playerTransform.position, moveSpeed, 0.35f);
        else
            sinMovement.MoveTowardsTargetDirection((transform.position - playerTransform.position).normalized, moveSpeed, 0.85f);

    }


    public override void StateOnTriggerExit(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Light") || otherCollider.CompareTag("LampLight"))
        {
            moveSpeed = moveSpeedScapingPlayer;
        }
    }

    IEnumerator WaitBeforeScared()
    {
        enemyAudio.PlayScaredAudio();
        yield return new WaitForSeconds(timeTargetingPlayer);

        isTargetingPlayer = false;
        moveSpeed = moveSpeedScapingPlayer;
        yield return new WaitForSeconds(timeBeforeScared);

        isWaitFinished = true;
    }


}
