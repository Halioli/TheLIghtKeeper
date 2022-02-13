using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawningState : EnemyState
{
    SpriteRenderer spriteRenderer;
    float spawnTime;
    bool isSpawning;
    bool isSpawningFinished;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnTime = 0.5f;
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

        Interpolator fadeLerp = new Interpolator(spawnTime, Interpolator.Type.SMOOTH);
        fadeLerp.ToMax();

        while (!fadeLerp.isMaxPrecise)
        {
            fadeLerp.Update(Time.deltaTime);
            fadeColor.a = fadeLerp.Value;
            spriteRenderer.material.color = fadeColor;
            yield return null;
        }

        isSpawning = false;
        isSpawningFinished = true;
    }


}
