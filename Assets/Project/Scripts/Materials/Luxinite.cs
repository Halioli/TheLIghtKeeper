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


    private void Awake()
    {
        SaveSystem.luxinites.Add(this);
    }
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

    public override void GetsMined(int damageAmount, int numberOfDrops)
    {
        StartCoroutine(FlashLightAppears());

        base.GetsMined(damageAmount, 1);

        if(healthSystem.GetHealth() <= 0)
        {
            hasBeenMined = true;
        }
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

    protected override void DropMineralItem()
    {
        ItemGameObject droppedMineralItem = Instantiate(mineralItemToDrop, GetDropSpawnPosition(), Quaternion.identity);
        droppedMineralItem.DropsRandom(false);
    }

}
