using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSpiderStateManager : MonoBehaviour
{
    Transform playerTransform;

    Dictionary<EnemyStates, EnemyState> states;
    public EnemyStates currentState { get; private set; }


    public void Init(Transform playerTransform)
    {
        this.playerTransform = playerTransform;

        states = new Dictionary<EnemyStates, EnemyState>();

        states.Add(EnemyStates.SPAWNING, GetComponent<EnemySpawningState>());

        states.Add(EnemyStates.WANDERING, GetComponent<EnemyWanderingState>());
        states[EnemyStates.WANDERING].SetPlayerTransform(playerTransform);

        states.Add(EnemyStates.AGGRO, GetComponent<EnemyAggroState>());
        states[EnemyStates.AGGRO].SetPlayerTransform(playerTransform);

        states.Add(EnemyStates.CHARGING, GetComponent<EnemyChargingState>());
        states[EnemyStates.CHARGING].SetPlayerTransform(playerTransform);

        states.Add(EnemyStates.LIGHT_ENTER, GetComponent<EnemyLightEnterState>());
        states[EnemyStates.LIGHT_ENTER].SetPlayerTransform(playerTransform);

        states.Add(EnemyStates.SCARED, GetComponent<EnemyScaredState>());
        states[EnemyStates.SCARED].SetPlayerTransform(playerTransform);

        states.Add(EnemyStates.DEATH, GetComponent<EnemyDeathState>());

        states.Add(EnemyStates.DESTROY, GetComponent<EnemyDestroyState>());

        currentState = EnemyStates.SPAWNING;
        states[currentState].StateStart();
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
        currentState = newState;
        states[currentState].StateStart();
    }

}
