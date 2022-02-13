using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyState : EnemyState
{
    protected override void StateDoStart()
    {
        Destroy(gameObject);
    }


}
