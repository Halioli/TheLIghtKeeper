using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeckoEnemy : EnemyMonster
{
    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        enemyAudio = GetComponent<EnemyAudio>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
}
