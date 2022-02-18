using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScaredState : EnemyState
{
    EnemyAudio enemyAudio;
    SinMovement sinMovement;
    SpriteRenderer spriteRenderer;
    Color fadeColor;

    [SerializeField] float moveSpeed;    
    [SerializeField] float fleeTime;

    bool isFleeing;
    bool isFleeingFinished;
    Vector2 fleeTargetDirection;



    private void Awake()
    {
        enemyAudio = GetComponent<EnemyAudio>();
        sinMovement = GetComponent<SinMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //moveSpeed = 20.0f;
        //fleeTime = 3.0f;
    }


    protected override void StateDoStart()
    {
        enemyAudio.PlayBanishAudio();
        ResetFleeing();
    }

    public override bool StateUpdate()
    {
        if (isFleeingFinished)
        {
            nextState = EnemyStates.DESTROY;
            return true;
        }
        else if (!isFleeing)
        {
            StartCoroutine(FadeOutAnimation());
        }

        return false;
    }

    public override void StateFixedUpdate()
    {
        sinMovement.MoveTowardsTargetDirection(fleeTargetDirection, moveSpeed);
    }


    private void ResetFleeing()
    {
        isFleeing = false;
        isFleeingFinished = false;

        fleeTargetDirection = (transform.position - playerTransform.position).normalized;
    }


    IEnumerator FadeOutAnimation()
    {
        isFleeing = true;
        fadeColor = spriteRenderer.material.color;

        Interpolator fadeLerp = new Interpolator(fleeTime, Interpolator.Type.SMOOTH);
        fadeLerp.ToMax();

        while (!fadeLerp.isMaxPrecise)
        {
            fadeLerp.Update(Time.deltaTime);
            fadeColor.a = fadeLerp.Inverse;
            spriteRenderer.material.color = fadeColor;
            yield return null;
        }

        isFleeing = false;
        isFleeingFinished = true;
    }


    public void ResetFadeAnimation()
    {
        if (!isFleeing) return;

        StopCoroutine(FadeOutAnimation());

        fadeColor = spriteRenderer.material.color;
        fadeColor.a = 1.0f;
        spriteRenderer.material.color = fadeColor;

        ResetFleeing();
    }


}
