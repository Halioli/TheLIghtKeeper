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

    private bool attacking = false;
    private bool attackingAnEnemy = false;

    private const float INVULNERABILITY_TIME = 0.5f;
    private float currentInvulnerabilityTime = INVULNERABILITY_TIME;
    private bool isInvulnerable = false;


    private Collider2D colliderDetectedByMouse = null;
    private Enemy enemyToAttack;

    //Particles
    public ParticleSystem playerBlood;


    // Audio
    public AudioSource audioSource;
    public AudioClip hurtedAudioClip;
    public AudioClip attackAudioClip;



    private void Start()
    {
        attackSystem = GetComponent<AttackSystem>();
        healthSystem = GetComponent<HealthSystem>();
        playerBlood.Stop();
    }

    void Update()
    {
        if (PlayerInputs.instance.PlayerClickedAttackButton() && !attacking)
        {
            PlayerInputs.instance.SetNewMousePosition();
            if (PlayerIsInReachToAttack(PlayerInputs.instance.mouseWorldPosition) && MouseClickedOnAnEnemy(PlayerInputs.instance.mouseWorldPosition))
            {
                SetEnemyToAttack();
            }
            StartAttacking();
        }
    }


    private bool PlayerIsInReachToAttack(Vector2 mousePosition)
    {
        float distancePlayerMouseClick = Vector2.Distance(mousePosition, transform.position);
        return distancePlayerMouseClick <= PlayerInputs.instance.playerReach;
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

        PlayerInputs.instance.SpawnSelectSpotAtTransform(enemyToAttack.transform);
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
        PlayerInputs.instance.canFlip = false;

        if (attackingAnEnemy)
        {
            DealDamageToEnemy();
        }

        while (attackingTime > 0.0f)
        {
            attackingTime -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        PlayerInputs.instance.canFlip = true;
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
        StartCoroutine(PlayerBloodParticleSystem());
    }

    IEnumerator Invulnerability()
    {
        isInvulnerable = true;
        gameObject.layer = LayerMask.NameToLayer("Default"); // Enemies layer can't collide with Default layer

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color original = spriteRenderer.color;
        Color transparent = spriteRenderer.color;
        transparent.a = 0.3f;

        while (currentInvulnerabilityTime >= 0.0f)
        {
            spriteRenderer.color = transparent;

            currentInvulnerabilityTime -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        spriteRenderer.color = original;
        currentInvulnerabilityTime = INVULNERABILITY_TIME;
        isInvulnerable = false;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void FlipPlayerSpriteFacingWhereToAttack()
    {
        if (playerStates.PlayerActionIsWalking())
            return;
        
        if ((transform.position.x < PlayerInputs.instance.mousePosition.x && !PlayerInputs.instance.facingLeft) ||
            (transform.position.x > PlayerInputs.instance.mousePosition.x && PlayerInputs.instance.facingLeft))
        {
            PlayerInputs.instance.facingLeft = !PlayerInputs.instance.facingLeft;
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    IEnumerator PlayerBloodParticleSystem()
    {
        playerBlood.Play();
        yield return new WaitForSeconds(0.3f);
        playerBlood.Stop();
    }
}
