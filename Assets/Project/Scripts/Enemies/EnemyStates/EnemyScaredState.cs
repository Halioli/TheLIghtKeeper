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



    private void Awake()
    {
        sinMovement = GetComponent<SinMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        //moveSpeed = 20.0f;
        //fleeTime = 3.0f;
    }


    protected override void StateDoStart()
    {
        ResetFleeing();
        animator.ResetTrigger("triggerMove");
        animator.SetTrigger("triggerIdle");
    }

    public override bool StateUpdate()
    {
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
            fadeColor.a = fadeLerp.Value;
            //spriteRenderer.material.color = fadeColor;
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
