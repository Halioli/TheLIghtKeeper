using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSpiderStateManager : EnemyStateManager
{

    public override void Init(Transform playerTransform)
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


}
