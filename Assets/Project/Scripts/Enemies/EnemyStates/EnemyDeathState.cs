using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyDeathState : EnemyState
{
    EnemyAudio enemyAudio;
    Animator animator;
    Collider2D collider;
    
    bool isDying;
    bool isDoneDying;


    private void Awake()
    {
        enemyAudio = GetComponent<EnemyAudio>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }


    protected override void StateDoStart()
    {
        enemyAudio.PlayDeathAudio();
        enemyAudio.StopFootstepsAudio();
        isDying = false;
        isDoneDying = false;
    }

    public override bool StateUpdate()
    {
        if (!isDying)
        {
            collider.enabled = false;
            StartDeathAnimation();
        }
        else if (isDoneDying)
        {
            nextState = EnemyStates.DESTROY;
            return true;
        }

        return false;
    }


    private void StartDeathAnimation()
    {
        transform.DOComplete();
        transform.DOPunchScale(new Vector3(0.5f, 0.5f), 1f, 10, 0.3f);
        transform.DOShakePosition(1f, 1.5f, 50);

        animator.SetTrigger("triggerDeath");
        isDying = true;
    }

    public void SetIsDoneDying()
    {
        isDoneDying = true;
    }



}
