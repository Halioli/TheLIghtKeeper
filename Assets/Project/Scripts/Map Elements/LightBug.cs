using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public enum LightBugMovement { LINEAR, CIRCLE };

public class LightBug : Enemy
{

    public static LightBug instance;

    Interpolator horizontalLerp;
    Interpolator verticalLerp;


    private Light2D[] pointLightBug;
    private bool isTurnedOn = false;

    public LightBugMovement lightBugMovement;

    //Linear Movement Parameters
    public float timeToReachEachPoint;
    public float initialPositionX;
    public float finalPositionX;
    public float initialPositionY;
    public float finalPositionY;

    //Circular Movement Parameters
    public float speed;
    public float width;
    public float height;

    private float timeCounter;

    private float initialIntensity = 0.3f;
    private float maxIntensity = 1f;
    private float time;
    private bool cycleFinished;

    void Start()
    {
        horizontalLerp = new Interpolator(timeToReachEachPoint, Interpolator.Type.SMOOTH);
        verticalLerp = new Interpolator(0.5f, Interpolator.Type.SMOOTH);

        pointLightBug = GetComponentsInChildren<Light2D>();
        healthSystem = GetComponent<BeingHealthSystem>();
        attackSystem = GetComponent<AttackSystem>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        cycleFinished = true;

    }

    void Update()
    {
        DoLightBugMovement();

        if (healthSystem.IsDead())
        {
            Die();
        }
    }

    private void DoLightBugMovement()
    {
        if(lightBugMovement == LightBugMovement.LINEAR)
        {
            UpdateInterpolators();
            transform.position = new Vector3(initialPositionX + (finalPositionX - initialPositionX) * horizontalLerp.Value, initialPositionY + (finalPositionX - initialPositionY) * horizontalLerp.Value, 0f);
            if (initialPositionX - finalPositionX != 0)
            {
                transform.position = new Vector3(transform.position.x, (transform.position.y + 1f) - (transform.position.y + 1f) * verticalLerp.Value, 0f);
            }
            else
            {
                transform.position = new Vector3((transform.position.x + 1f) - (transform.position.x + 1f) * verticalLerp.Value, transform.position.y, 0f);
            }
        }
        else
        {
            timeCounter += Time.deltaTime * speed;
            transform.position = new Vector3(Mathf.Cos(timeCounter) * width, Mathf.Sin(timeCounter) * height, 0);

            
        }

    }

    //IEnumerator FlashLightAppears()
    //{
    //    time = 0f;
    //    while (time < 1)
    //    {
    //        pointLightBug[0].intensity = Mathf.Lerp(initialIntensity, maxIntensity, time);
    //        pointLightBug[1].intensity = Mathf.Lerp(initialIntensity, maxIntensity, time);

    //        time += Time.deltaTime;
    //        yield return new WaitForSeconds(Time.deltaTime);
    //    }
    //    time = 0f;
    //    while (time < 1)
    //    {
    //        pointLightBug[0].intensity = Mathf.Lerp(maxIntensity, initialIntensity, time);
    //        pointLightBug[1].intensity = Mathf.Lerp(maxIntensity, initialIntensity, time);

    //        time += Time.deltaTime;
    //        yield return new WaitForSeconds(Time.deltaTime);
    //    }
    //}

    protected override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }

    private void UpdateInterpolators()
    {
        horizontalLerp.Update(Time.deltaTime);
        if (horizontalLerp.isMinPrecise)
        {
            FlipSprite();
            horizontalLerp.ToMax();
        }

        else if (horizontalLerp.isMaxPrecise) 
        {
            FlipSprite();
            horizontalLerp.ToMin();
        }


        verticalLerp.Update(Time.deltaTime);
        if (verticalLerp.isMinPrecise)
        {
            FlipSprite();
            verticalLerp.ToMax();
        }
        else if (verticalLerp.isMaxPrecise)
        {
            FlipSprite();
            verticalLerp.ToMin();
        }
    }

    public void FlipSprite()
    {
           spriteRenderer.flipX = !spriteRenderer.flipX;
    }

}

