using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightBug : Enemy
{
    Interpolator lerp;

    private Light2D[] pointLightBug;
    private bool isTurnedOn = false;
    
    void Start()
    {
        lerp = new Interpolator(5f, Interpolator.Type.SMOOTH);
        pointLightBug = GetComponentsInChildren<Light2D>();
        healthSystem = GetComponent<BeingHealthSystem>();
        attackSystem = GetComponent<AttackSystem>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        LightBugMovement();
        if (isTurnedOn)
        {
            FadeOut();
        }
        else
        {
            FadeIn();
        }

        if (healthSystem.IsDead())
        {
            Die();
        }
    }

    private void LightBugMovement()
    {
        lerp.Update(Time.deltaTime);
        if (lerp.isMinPrecise)
            lerp.ToMax();
        else if (lerp.isMaxPrecise)
            lerp.ToMin();

        transform.position = new Vector3(10f - 20f * lerp.Value, 0f, 0f);
    }

    IEnumerator FadeIn()
    {
        Interpolator lightFadeLerp = new Interpolator(1f, Interpolator.Type.SIN);
        lightFadeLerp.ToMax();
        while (!lightFadeLerp.isMaxPrecise)
        {
            lightFadeLerp.Update(Time.deltaTime);
            //DO STUFF HERE
            pointLightBug[0].intensity = lightFadeLerp.Inverse + 0.5f;
            pointLightBug[1].intensity = lightFadeLerp.Inverse * 0.6f;
            //WAIT A FRAME
            yield return null;
        }

        isTurnedOn = true;
    }

    IEnumerator FadeOut()
    {
        Interpolator lightFadeLerp = new Interpolator(1f, Interpolator.Type.SIN);
        lightFadeLerp.ToMax();

        while (!lightFadeLerp.isMaxPrecise)
        {
            lightFadeLerp.Update(Time.deltaTime);
            //DO STUFF HERE
            pointLightBug[0].intensity = lightFadeLerp.Value + 0.5f;
            pointLightBug[1].intensity = (lightFadeLerp.Value + 0.3f) * 0.6f;
            //WAIT A FRAME
            yield return null;
        }

        isTurnedOn = false;
    }

    protected override void Die()
    {
        Debug.Log("murio");
        base.Die();
        Destroy(gameObject);
    }
}
