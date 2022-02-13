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
    protected GameObject playerGameObject;


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


    public void SetPlayerGameObject(GameObject playerGameObject)
    {
        //this.playerGameObject = playerGameObject;
        Debug.Log("SetPlayerGameObject");

        this.playerGameObject = GameObject.FindGameObjectWithTag("Player");
    }

}
