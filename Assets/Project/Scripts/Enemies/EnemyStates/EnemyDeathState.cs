using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyState
{
    Animator animator;
    
    bool isDying;
    bool isDoneDying;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    protected override void StateDoStart()
    {
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
        animator.SetTrigger("isDead");

        isDying = true;
    }

    public void SetIsDoneDying()
    {
        isDoneDying = true;
    }



}
