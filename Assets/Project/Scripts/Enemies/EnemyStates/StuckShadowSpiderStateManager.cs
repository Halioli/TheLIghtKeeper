using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckShadowSpiderStateManager : ShadowSpiderStateManager
{
    public override void Init(Transform playerTransform)
    {
        this.playerTransform = playerTransform;

        states = new Dictionary<EnemyStates, EnemyState>();

        states.Add(EnemyStates.STUCK, GetComponent<EnemyStuckState>());

        states.Add(EnemyStates.DEATH, GetComponent<EnemyDeathState>());

        states.Add(EnemyStates.DESTROY, GetComponent<EnemyDestroyState>());

        currentState = EnemyStates.STUCK;
        states[currentState].StateStart();
    }

}
