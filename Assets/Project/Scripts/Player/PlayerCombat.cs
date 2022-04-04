using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCombat : PlayerBase
{
    // Private Attributes
    private const float ATTACK_TIME_DURATION = 0.22f;
    private float ATTACK_COOLDOWN = 0.7f;

    private bool canAttack = true;

    private const float INVULNERABILITY_TIME = 0.5f;
    private float currentInvulnerabilityTime = INVULNERABILITY_TIME;
    private bool isInvulnerable = false;

    private PlayerAreas playerAreas;
    private InGameHUDHandler inGameHUD;

    protected AttackSystem attackSystem;
    protected HealthSystem healthSystem;

    // Public Attributes
    //public GameObject attackArea;
    public HUDHandler hudHandler;
    public bool targetWasHitAlready = false;

    //Particles
    public ParticleSystem playerBlood;
    public Animator animator;

    // Events
    public delegate void PlayerAttackSound();
    public static event PlayerAttackSound playerAttackEvent;
    public static event PlayerAttackSound playerMissesAttackEvent;
    public static event PlayerAttackSound playerReceivesDamageEvent;

    public delegate void PlayerReceivesDamage();
    public static event PlayerReceivesDamage OnReceivesDamage;

    private void Start()
    {
        playerAreas = GetComponent<PlayerAreas>();
        attackSystem = GetComponent<AttackSystem>();
        healthSystem = GetComponent<HealthSystem>();
        inGameHUD = GetComponentInChildren<InGameHUDHandler>();
        animator = GetComponent<Animator>();
        playerBlood.Stop();
    }

    //void Update()
    //{
    //    if (PlayerInputs.instance.PlayerClickedAttackButton() && canAttack && playerStates.PlayerStateIsFree())
    //    {
    //        StartAttacking();
    //    }
    //}

    private void StartAttacking()
    {
        FlipPlayerSpriteFacingWhereToAttack();
        playerStates.SetCurrentPlayerAction(PlayerAction.ATTACKING);
        StartCoroutine(Attacking());
        StartCoroutine(AttackCooldown());
    }


    IEnumerator Attacking()
    {
        PlayerInputs.instance.canFlip = false;
        animator.SetBool("isAttacking", true);

        playerAttackEvent();

        playerAreas.DoSpawnAttackArea();
        yield return new WaitForSeconds(ATTACK_TIME_DURATION);

        PlayerInputs.instance.canFlip = true;
        animator.SetBool("isAttacking", false);
        ResetAttack();
    }

    private void ResetAttack()
    {
        playerStates.SetCurrentPlayerState(PlayerState.FREE);
        playerStates.SetCurrentPlayerAction(PlayerAction.IDLE);
    }

    public void DealDamageToEnemy(Enemy enemy)
    {
        enemy.ReceiveDamage(attackSystem.attackValue);
        enemy.GetsPushed((enemy.transform.position - transform.position).normalized, attackSystem.pushValue);
    }

    public void ReceiveDamage(int damageValue)
    {
        if (isInvulnerable)
        {
            return;
        }
        else
        {
            StartCoroutine(Invulnerability());

            if(OnReceivesDamage != null)
                OnReceivesDamage();
            
            //inGameHUD.DoReceiveDamageFadeAndShake();
            //hudHandler.ShowRecieveDamageFades();
        }

        healthSystem.ReceiveDamage(damageValue);

        transform.DOPunchScale(new Vector3(-0.4f, 0.2f, 0), 0.5f);
        transform.DOPunchRotation(new Vector3(0, 0, 10), 0.2f);

        playerReceivesDamageEvent();

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
        Vector2 mousePosition = PlayerInputs.instance.GetMousePositionInWorld();
        
        if ((transform.position.x < mousePosition.x && !PlayerInputs.instance.facingLeft) ||
            (transform.position.x > mousePosition.x && PlayerInputs.instance.facingLeft))
        {
            Vector2 direction = mousePosition - (Vector2)transform.position;
            PlayerInputs.instance.FlipSprite(direction);
        }
    }

    IEnumerator PlayerBloodParticleSystem()
    {
        playerBlood.Play();
        yield return new WaitForSeconds(0.3f);
        playerBlood.Stop();
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(ATTACK_COOLDOWN);
        canAttack = true;
    }


    public void TargetWasHit()
    {
        //if (!targetWasHitAlready)
        //{
        //    targetWasHitAlready = true;
        //    playerAttackEvent();
        //}
    }

}
