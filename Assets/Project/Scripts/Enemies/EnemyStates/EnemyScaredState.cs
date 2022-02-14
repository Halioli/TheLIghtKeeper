using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScaredState : EnemyState
{
    SinMovement sinMovement;
    SpriteRenderer spriteRenderer;
    Color fadeColor;

    [SerializeField] float moveSpeed;    
    [SerializeField] float fleeTime;

    bool isFleeing;
    bool isFleeingFinished;


    private void Awake()
    {
        sinMovement = GetComponent<SinMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //moveSpeed = 20.0f;
        //fleeTime = 3.0f;
    }


    protected override void StateDoStart()
    {
        isFleeing = false;
        isFleeingFinished = false;
    }

    public override bool StateUpdate()
    {
        if (!isFleeing)
        {
            StartCoroutine(FadeOutAnimation());
        }
        else if (!isFleeingFinished)
        {
            nextState = EnemyStates.DEATH;
            return true;
        }


        return false;
    }

    public override void StateFixedUpdate()
    {
        sinMovement.MoveTowardsTargetPosition(-playerTransform.position, moveSpeed);
    }


    IEnumerator FadeOutAnimation()
    {
        isFleeing = true;
        fadeColor = spriteRenderer.material.color;

        Interpolator fadeLerp = new Interpolator(fleeTime, Interpolator.Type.SMOOTH);
        fadeLerp.ToMin();

        while (!fadeLerp.isMinPrecise)
        {
            fadeLerp.Update(Time.deltaTime);
            fadeColor.a = fadeLerp.Value;
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

        StateDoStart();
    }


}
