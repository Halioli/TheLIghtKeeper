using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCombat : PlayerBase
{
    // Private Attributes
    private const float ATTACK_TIME_DURATION = 0.22f;
    private float attackingTime = ATTACK_TIME_DURATION;
    private float ATTACK_COOLDOWN = 0.7f;

    private bool canAttack = true;

    private const float INVULNERABILITY_TIME = 0.5f;
    private float currentInvulnerabilityTime = INVULNERABILITY_TIME;
    private bool isInvulnerable = false;
    private float pushForce = 50f;

    private PlayerAreas playerAreas;

    protected AttackSystem attackSystem;
    protected HealthSystem healthSystem;

    // Public Attributes
    public GameObject attackArea;

    //Particles
    public ParticleSystem playerBlood;
    public Animator animator;
    public GameObject swordLight;

    //Audio
    public AudioSource audioSource;
    public AudioClip hurtedAudioClip;
    public AudioClip attackAudioClip;

    private void Start()
    {
        playerAreas = GetComponent<PlayerAreas>();
        attackSystem = GetComponent<AttackSystem>();
        healthSystem = GetComponent<HealthSystem>();
        playerBlood.Stop();
    }

    void Update()
    {
        if (PlayerInputs.instance.PlayerClickedAttackButton() && canAttack)
        {
            StartAttacking();
        }
    }

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
        swordLight.SetActive(true);

        audioSource.pitch = Random.Range(0.8f, 1.3f);
        audioSource.clip = attackAudioClip;
        audioSource.Play();

        playerAreas.DoSpawnAttackArea();
        while (attackingTime > 0.0f)
        {
            attackingTime -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        PlayerInputs.instance.canFlip = true;
        animator.SetBool("isAttacking", false);
        swordLight.SetActive(false);
        ResetAttack();
    }

    private void ResetAttack()
    {
        attackingTime = ATTACK_TIME_DURATION;

        playerStates.SetCurrentPlayerState(PlayerState.FREE);
        playerStates.SetCurrentPlayerAction(PlayerAction.IDLE);
    }

    public void DealDamageToEnemy(Enemy enemy)
    {
        enemy.ReceiveDamage(attackSystem.attackValue);
        enemy.GetsPushed((enemy.transform.position - transform.position).normalized, pushForce);
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
}
