using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSpiderStateManager : MonoBehaviour
{
    GameObject playerGameObject;

    Dictionary<EnemyStates, EnemyState> states;
    EnemyStates currentState;


    public void Init()
    {
        states = new Dictionary<EnemyStates, EnemyState>();

        states.Add(EnemyStates.SPAWNING, GetComponent<EnemySpawningState>());

        states.Add(EnemyStates.WANDERING, GetComponent<EnemyWanderingState>());
        states[EnemyStates.WANDERING].SetPlayerGameObject(playerGameObject);

        states.Add(EnemyStates.AGGRO, GetComponent<EnemyAggroState>());
        states[EnemyStates.AGGRO].SetPlayerGameObject(playerGameObject);

        states.Add(EnemyStates.CHARGING, GetComponent<EnemyChargingState>());
        states[EnemyStates.CHARGING].SetPlayerGameObject(playerGameObject);

        states.Add(EnemyStates.SCARED, GetComponent<EnemyScaredState>());
        states[EnemyStates.SCARED].SetPlayerGameObject(playerGameObject);

        states.Add(EnemyStates.DEATH, GetComponent<EnemyDeathState>());

        states.Add(EnemyStates.DESTROY, GetComponent<EnemyDestroyState>());

        currentState = EnemyStates.SPAWNING;
        states[currentState].StateStart();

        string msg = "number of states: " + states.Count;
        Debug.Log(msg);
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



    public void ForceDeathState()
    {
        currentState = EnemyStates.DEATH;
        states[currentState].StateStart();
    }

}
