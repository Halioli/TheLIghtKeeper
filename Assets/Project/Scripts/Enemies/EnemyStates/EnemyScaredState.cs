using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class EnemyScaredState : EnemyState
{
    SinMovement sinMovement;
    SpriteRenderer spriteRenderer;
    Color fadeColor;
    Animator animator;

    [SerializeField] float moveSpeed;    
    [SerializeField] float fleeTime;
    [SerializeField] float distanceToStartBanishing;

    bool isBanishing;
    bool isBanishingFinished;
    Vector2 fleeTargetDirection;

    bool isPlayerNotInLight;


    private void Awake()
    {
        sinMovement = GetComponent<SinMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        //moveSpeed = 20.0f;
        //fleeTime = 3.0f;
    }

    private void OnEnable()
    {
        DarknessSystem.OnPlayerNotInLight += () => isPlayerNotInLight = true;
    }

    private void OnDisable()
    {
        DarknessSystem.OnPlayerNotInLight -= () => isPlayerNotInLight = true;
    }


    protected override void StateDoStart()
    {
        isPlayerNotInLight = false;
        ResetFleeing();
        animator.ResetTrigger("triggerMove");
        animator.SetTrigger("triggerIdle");
    }

    public override bool StateUpdate()
    {
        if (isPlayerNotInLight)
        {
            nextState = EnemyStates.WANDERING;
            return true;
        }
        if (isBanishingFinished)
        {
            nextState = EnemyStates.DESTROY;
            return true;
        }
        else if (!isBanishing && IsFarEnoughToStartBanishing())
        {
            StartCoroutine(FadeOutAnimation());
        }

        return false;
    }

    public override void StateFixedUpdate()
    {
        sinMovement.MoveTowardsTargetDirection(fleeTargetDirection, moveSpeed, 1.35f);
    }


    private void ResetFleeing()
    {
        isBanishing = false;
        isBanishingFinished = false;

        fleeTargetDirection = (transform.position - playerTransform.position).normalized;
    }


    private bool IsFarEnoughToStartBanishing()
    {
        return distanceToStartBanishing < Vector2.Distance(transform.position, playerTransform.position);
    }

    IEnumerator FadeOutAnimation()
    {
        isBanishing = true;
        fadeColor = spriteRenderer.material.color;

        Interpolator fadeLerp = new Interpolator(fleeTime, Interpolator.Type.SMOOTH);
        fadeLerp.ToMax();

        while (!fadeLerp.isMaxPrecise)
        {
            fadeLerp.Update(Time.deltaTime);
            fadeColor.a = fadeLerp.Inverse;
            spriteRenderer.color = fadeColor;
            yield return null;
        }

        isBanishing = false;
        isBanishingFinished = true;
    }


    public void ResetFadeAnimation()
    {
        if (!isBanishing) return;

        StopCoroutine(FadeOutAnimation());

        fadeColor = spriteRenderer.material.color;
        fadeColor.a = 1.0f;
        //spriteRenderer.material.color = fadeColor;

        ResetFleeing();
    }



}
