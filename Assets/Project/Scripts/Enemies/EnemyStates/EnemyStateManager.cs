using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    protected Dictionary<EnemyStates, EnemyState> states;
    public EnemyStates currentState { get; protected set; }


    protected Transform playerTransform;


    public virtual void Init(Transform playerTransform) 
    {
    }


    private void Update()
    {
        if (!states[currentState].StateUpdate()) return;

        currentState = states[currentState].nextState;
        states[currentState].StateStart();  
    }

    private void FixedUpdate()
    {
        states[currentState].StateFixedUpdate();
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        states[currentState].StateOnTriggerEnter(otherCollider);
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        states[currentState].StateOnTriggerExit(otherCollider);
    }


    public void ForceState(EnemyStates newState)
    {
        if (!states.ContainsKey(newState)) return;

        if (currentState == EnemyStates.DEATH || currentState == EnemyStates.DESTROY) return;

        currentState = newState;
        states[currentState].StateStart();
    }


}
