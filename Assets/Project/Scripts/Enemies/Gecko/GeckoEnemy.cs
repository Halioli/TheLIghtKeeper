using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeckoEnemy : EnemyMonster
{
    bool isDeadAlready = false;
    [SerializeField] Animator geckoAnimator;


    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        enemyAudio = GetComponent<EnemyAudio>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if (healthSystem.IsDead() && !isDeadAlready)
        {
            geckoAnimator.SetBool("death", true);
        }
    }


}
