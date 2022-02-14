using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyStates
{
    NONE,
    SPAWNING,
    WANDERING,
    AGGRO,
    CHARGING,
    SCARED,
    DEATH,
    DESTROY
}

public class EnemyState : MonoBehaviour
{

    public EnemyStates nextState { get; protected set; }
    protected Transform playerTransform;


    public void StateStart()
    {
        nextState = EnemyStates.NONE;
        StateDoStart();
    }
    protected virtual void StateDoStart()
    {
    }


    public virtual bool StateUpdate()
    {
        return false;
    }

    public virtual void StateFixedUpdate()
    {
    }

    public virtual void StateOnTriggerEnter(Collider2D otherCollider)
    {
    }


    public void SetPlayerTransform(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }

}
