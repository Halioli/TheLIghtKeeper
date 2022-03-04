using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyState : EnemyState
{
    public delegate void EnemyDestroyAction();
    public static event EnemyDestroyAction OnEnemyDestroy;

    protected override void StateDoStart()
    {
        if (OnEnemyDestroy != null) OnEnemyDestroy();

        Destroy(gameObject);
    }


}
