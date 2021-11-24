using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : PlayerInputs
{  
    // Private Attributes
    private AttackSystem attackSystem;
    private const float ATTACK_TIME_DURATION = 0.8f;
    private float attackingTime = ATTACK_TIME_DURATION;

    private float attackReachRadius = 3f;
    private bool isAttacking;

    private Collider2D colliderDetectedByMouse = null;
    private Enemy enemyToAttack;

    private void Start()
    {
        attackSystem = GetComponent<AttackSystem>();

    }

    void Update()
    {
        if (PlayerClickedAttackButton() && !isAttacking)
        {
            SetNewMousePosition();
            if (PlayerIsInReachToAttack(mouseWorldPosition) && MouseClickedOnAnEnemy(mouseWorldPosition))
            {
                SetEnemyToAttack();
                StartAttacking();
            }
        }
    }


    private bool PlayerIsInReachToAttack(Vector2 mousePosition)
    {
        float distancePlayerMouseClick = Vector2.Distance(mousePosition, transform.position);
        return distancePlayerMouseClick <= attackReachRadius;
    }

    private bool MouseClickedOnAnEnemy(Vector2 mousePosition)
    {
        colliderDetectedByMouse = Physics2D.OverlapCircle(mousePosition, 0.05f);
        return colliderDetectedByMouse != null && colliderDetectedByMouse.gameObject.CompareTag("Enemy");
    }

    private void SetEnemyToAttack()
    {
        enemyToAttack = colliderDetectedByMouse.gameObject.GetComponent<Enemy>();
    }

    private void StartAttacking()
    {
        isAttacking = true;
        StartCoroutine("Attacking");
    }


    IEnumerator Attacking()
    {
        attackSystem.DamageHealthSystemWithAttackValue(enemyToAttack.GetComponent<HealthSystem>());
        while (attackingTime > 0.0f)
        {
            attackingTime -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        ResetAttack();
    }

    private void ResetAttack()
    {
        attackingTime = ATTACK_TIME_DURATION;
        isAttacking = false;
    }
}
