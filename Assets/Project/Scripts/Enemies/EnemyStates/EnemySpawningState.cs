using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemySpawningState : EnemyState
{
    SpriteRenderer spriteRenderer;
    float spawnTime;
    bool isSpawning;
    bool isSpawningFinished;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnTime = 1.0f;
    }


    protected override void StateDoStart()
    {
        isSpawning = false;
        isSpawningFinished = false;
    }


    public override bool StateUpdate()
    {
        if (!isSpawning && !isSpawningFinished)
        {
            StartCoroutine(SpawnAnimation());
        }
        else if (isSpawningFinished)
        {
            nextState = EnemyStates.WANDERING;
            return true;
        }

        return false;
    }

    public override void StateFixedUpdate()
    {
    }


    IEnumerator SpawnAnimation()
    {
        isSpawning = true;
        Color fadeColor = spriteRenderer.material.color;
        fadeColor.a = 0;
        spriteRenderer.material.color = fadeColor;


        Interpolator fadeLerp = new Interpolator(spawnTime, Interpolator.Type.LINEAR);
        fadeLerp.ToMax();

        while (!fadeLerp.isMaxPrecise)
        {
            fadeLerp.Update(Time.deltaTime);
            fadeColor.a = fadeLerp.Value;
            spriteRenderer.color = fadeColor;
            yield return null;
        }
        fadeColor.a = fadeLerp.Value;
        spriteRenderer.material.color = fadeColor;

        isSpawning = false;
        isSpawningFinished = true;
    }


}
