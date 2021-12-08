using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Experimental.Rendering.Universal;


public class Luxinite : Ore
{
    public Light2D flashLight;

    private float initialIntensity;
    private float maxIntensity;
    private float time;

    void Start()
    {
        breakState = OreState.WHOLE;

        currentSpriteIndex = 0;
        currentSprite = spriteList[currentSpriteIndex];

        healthSystem = GetComponent<HealthSystem>();
        foreach (ParticleSystem particleSystem in oreParticleSystem)
        {
            particleSystem.Stop();
        }

        initialIntensity = 0f;
        maxIntensity = 3f;
    }

    public override void GetsMined(int damageAmount)
    {
        StartCoroutine(FlashLightAppears());

        transform.DOPunchScale(new Vector3(-0.6f, -0.6f, 0), 0.40f);
        // Damage the Ore
        healthSystem.ReceiveDamage(damageAmount);
        // Update ore Sprite
        ProgressNAmountOfSprites(damageAmount);

        if (healthSystem.IsDead())
        {
            breakState = OreState.BROKEN;

            // Drop mineralItemToDrop
            DropMineralItem();

            // Start disappear coroutine
            StartCoroutine("Disappear");
        }
        UpdateCurrentSprite();
        StartCoroutine("PlayBreakParticles");
    }

    IEnumerator FlashLightAppears()
    {
        time = 0f;
        while (time < 1)
        {
            flashLight.intensity = Mathf.Lerp(initialIntensity, maxIntensity, time);
            time += 10f * Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        time = 0f;
        while (time < 1)
        {
            flashLight.intensity = Mathf.Lerp(maxIntensity, initialIntensity, time);
            time += 10f * Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
