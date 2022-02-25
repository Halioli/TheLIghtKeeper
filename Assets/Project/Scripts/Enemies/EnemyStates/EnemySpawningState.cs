using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemySpawningState : EnemyState
{
    SpriteRenderer spriteRenderer;
    Collider2D collider;
    float spawnTime;
    bool isSpawning;
    bool isSpawningFinished;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
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
        collider.enabled = false;
        Color fadeColor = spriteRenderer.material.color;
        fadeColor.a = 0;
        spriteRenderer.material.color = fadeColor;
        fadeColor.a = 1;
        spriteRenderer.DOBlendableColor(fadeColor, spawnTime);
        yield return new WaitForSeconds(spawnTime);

        //Interpolator fadeLerp = new Interpolator(spawnTime, Interpolator.Type.LINEAR);
        //fadeLerp.ToMax();

        //while (!fadeLerp.isMaxPrecise)
        //{
        //    fadeLerp.Update(Time.deltaTime);
        //    fadeColor.a = fadeLerp.Value;
        //    spriteRenderer.material.color = fadeColor;
            
        //    yield return null;
        //}
        //fadeColor.a = fadeLerp.Value;
        //spriteRenderer.material.color = fadeColor;

        isSpawning = false;
        isSpawningFinished = true;
        collider.enabled = true;
    }


}
