using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyState
{
    EnemyAudio enemyAudio;
    Animator animator;
    
    bool isDying;
    bool isDoneDying;


    private void Awake()
    {
        enemyAudio = GetComponent<EnemyAudio>();
        animator = GetComponent<Animator>();
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
        animator.SetTrigger("triggerDeath");
        isDying = true;
    }

    public void SetIsDoneDying()
    {
        isDoneDying = true;
    }



}
