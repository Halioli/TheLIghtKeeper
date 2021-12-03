using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCombat : PlayerBase
{  
    // Private Attributes
    private const float ATTACK_TIME_DURATION = 0.5f;
    private float attackingTime = ATTACK_TIME_DURATION;

    protected AttackSystem attackSystem; 
    protected HealthSystem healthSystem;

    private float attackReachRadius = 3f;
    private bool attacking = false;
    private bool attackingAnEnemy = false;

    private const float INVULNERABILITY_TIME = 1.0f;
    private float currentInvulnerabilityTime = INVULNERABILITY_TIME;
    private bool isInvulnerable = false;


    private Collider2D colliderDetectedByMouse = null;
    private Enemy enemyToAttack;



    // Audio
    public AudioSource audioSource;
    public AudioClip hurtedAudioClip;
    public AudioClip attackAudioClip;



    private void Start()
    {
        attackSystem = GetComponent<AttackSystem>();
        healthSystem = GetComponent<HealthSystem>();
    }

    void Update()
    {
        if (playerInputs.PlayerClickedAttackButton() && !attacking)
        {
            playerInputs.SetNewMousePosition();
            if (PlayerIsInReachToAttack(playerInputs.mouseWorldPosition) && MouseClickedOnAnEnemy(playerInputs.mouseWorldPosition))
            {
                SetEnemyToAttack();
            }
            StartAttacking();
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
        attackingAnEnemy = true;
        enemyToAttack = colliderDetectedByMouse.gameObject.GetComponent<Enemy>();
    }

    private void StartAttacking()
    {
        attacking = true;
        FlipPlayerSpriteFacingWhereToAttack();
        playerStates.SetCurrentPlayerAction(PlayerAction.ATTACKING);
        StartCoroutine("Attacking");
    }


    IEnumerator Attacking()
    {
        playerInputs.canFlip = false;

        if (attackingAnEnemy)
        {
            DealDamageToEnemy();
        }

        while (attackingTime > 0.0f)
        {
            attackingTime -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        playerInputs.canFlip = true;
        ResetAttack();
    }

    private void ResetAttack()
    {
        attackingTime = ATTACK_TIME_DURATION;
        attacking = false;
        attackingAnEnemy = false;

        playerStates.SetCurrentPlayerState(PlayerState.FREE);
        playerStates.SetCurrentPlayerAction(PlayerAction.IDLE);
    }



    public void DealDamageToEnemy()
    {
        enemyToAttack.GetComponent<Enemy>().ReceiveDamage(attackSystem.attackValue);

        audioSource.pitch = Random.Range(0.8f, 1.3f);
        audioSource.clip = attackAudioClip;
        audioSource.Play();
    }


    public void ReceiveDamage(int damageValue)
    {
        if (isInvulnerable)
        {
            return;
        }
        else
        {
            StartCoroutine("Invulnerability");
        }

        healthSystem.ReceiveDamage(damageValue);

        transform.DOPunchScale(new Vector3(-0.4f, 0.2f, 0), 0.5f);
        transform.DOPunchRotation(new Vector3(0, 0, 10), 0.2f);

        audioSource.pitch = Random.Range(0.8f, 1.3f);
        audioSource.clip = hurtedAudioClip;
        audioSource.Play();
    }

    IEnumerator Invulnerability()
    {
        isInvulnerable = true;

        while (currentInvulnerabilityTime >= 0.0f)
        {
            currentInvulnerabilityTime -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        currentInvulnerabilityTime = INVULNERABILITY_TIME;
        isInvulnerable = false;
    }

    private void FlipPlayerSpriteFacingWhereToAttack()
    {
        if (playerStates.PlayerActionIsWalking())
            return;
        
        if ((transform.position.x < playerInputs.mousePosition.x && !playerInputs.facingLeft) ||
            (transform.position.x > playerInputs.mousePosition.x && playerInputs.facingLeft))
        {
            playerInputs.facingLeft = !playerInputs.facingLeft;
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }
}
