using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScaredState : EnemyState
{
    SinMovement sinMovement;
    float moveSpeed;

    SpriteRenderer spriteRenderer;
    float fleeTime;

    bool isFleeing;
    bool isFleeingFinished;


    private void Awake()
    {
        sinMovement = GetComponent<SinMovement>();
        moveSpeed = 40.0f;

        spriteRenderer = GetComponent<SpriteRenderer>();
        fleeTime = 3.0f;
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
        sinMovement.MoveTowardsTargetPosition(-playerGameObject.transform.position, moveSpeed);
    }


    IEnumerator FadeOutAnimation()
    {
        isFleeing = true;
        Color fadeColor = spriteRenderer.material.color;

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

}
