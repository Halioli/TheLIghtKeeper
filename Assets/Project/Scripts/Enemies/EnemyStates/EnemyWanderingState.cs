using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWanderingState : EnemyState
{
    EnemyAudio enemyAudio;
    SinMovement sinMovement;
    Animator animator;

    bool isMoving;
    bool isWanderingDone;
    float wanderingTime;
    [SerializeField] float minimumWanderDistance;
    [SerializeField] float wanderingWaitTime;
    [SerializeField] float wanderingRadius;
    [SerializeField] float moveSpeed;

    Vector2 wanderingCentrePosition;
    Vector2 targetPosition;

    [SerializeField] float distanceToAggro;
    float distanceToStartBanishing = 45f;
    bool isTouchingLight;

    bool isPlayerInLight;


    private void Awake()
    {
        enemyAudio = GetComponent<EnemyAudio>();
        sinMovement = GetComponent<SinMovement>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        DarknessSystem.OnPlayerEntersLight += () => isPlayerInLight = true;
        DarknessSystem.OnPlayerNotInLight += () => isPlayerInLight = false;
    }

    private void OnDisable()
    {
        DarknessSystem.OnPlayerEntersLight -= () => isPlayerInLight = true;
        DarknessSystem.OnPlayerNotInLight -= () => isPlayerInLight = false;
    }



    protected override void StateDoStart()
    {
        isMoving = true;
        isTouchingLight = false;
        isWanderingDone = true;

        isPlayerInLight = DarknessSystem.instance.playerInLight;

        SetWanderingCentrePosition();
        StartCoroutine(WaitForNewWanderingTargetPosition());
    }

    public override bool StateUpdate()
    {
        if (isTouchingLight)
        {
            if (!isMoving) StopCoroutine(WaitForNewWanderingTargetPosition());
            nextState = EnemyStates.LIGHT_ENTER;
            return true;
        }
        else if (isMoving && isWanderingDone)
        {
            StartCoroutine(WaitForNewWanderingTargetPosition());
        }
        else if (IsCloseToPlayerPosition()) {
            
            if (!isMoving) StopCoroutine(WaitForNewWanderingTargetPosition());
            enemyAudio.PlayFootstepsAudio();

            if (isPlayerInLight) 
            {
                nextState = EnemyStates.SCARED;
                return true;
            }
            nextState = EnemyStates.AGGRO;
            return true;
        }
        else if(distanceToStartBanishing < Vector2.Distance(transform.position, playerTransform.position))
        {
            nextState = EnemyStates.SCARED;
            return true;
        }

        return false;
    }

    public override void StateFixedUpdate()
    {
        if (!isMoving) return;
        
        sinMovement.MoveTowardsTargetPosition(targetPosition, moveSpeed);
    }

    public override void StateOnTriggerEnter(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.layer == LayerMask.NameToLayer("Light"))
        {
            isTouchingLight = true;
        }
    }


    private void SetWanderingCentrePosition()
    {
        wanderingCentrePosition = transform.position;
    }
    private void SetWanderingTargetPosition()
    {
        do {
            targetPosition = wanderingCentrePosition + (Random.insideUnitCircle * wanderingRadius);
        } while (Vector2.Distance(targetPosition, transform.position) < minimumWanderDistance);
    }


    IEnumerator WaitForNewWanderingTargetPosition()
    {
        isWanderingDone = false;
        isMoving = false;
        enemyAudio.StopFootstepsAudio();
        animator.ResetTrigger("triggerMove");
        animator.SetTrigger("triggerIdle");

        yield return new WaitForSeconds(wanderingWaitTime);

        isMoving = true;
        enemyAudio.PlayFootstepsAudio();
        animator.ResetTrigger("triggerIdle");
        animator.SetTrigger("triggerMove");
        SetWanderingTargetPosition();

        wanderingTime = Vector2.Distance(targetPosition, transform.position) / moveSpeed;
        yield return new WaitForSeconds(wanderingTime);
        isWanderingDone = true;
    }


    private bool IsCloseToPlayerPosition()
    {
        return Vector2.Distance(playerTransform.position, transform.position) <= distanceToAggro;
    }


}
