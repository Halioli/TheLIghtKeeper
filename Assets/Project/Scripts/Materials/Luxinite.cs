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
    public bool hasBeenMinedLux;

    private void Awake()
    {
        SaveSystem.luxinites.Add(this);

        breakState = OreState.WHOLE;

        currentSpriteIndex = 0;
        currentSprite = spriteList[currentSpriteIndex];

        healthSystem = GetComponent<HealthSystem>();
    }
    void Start()
    {
      
        foreach (ParticleSystem particleSystem in oreParticleSystem)
        {
            particleSystem.Stop();
        }

        initialIntensity = 0f;
        maxIntensity = 3f;

        if (hasBeenMinedLux)
        {
            this.gameObject.SetActive(false);
            breakState = OreState.BROKEN;
        }
    }

    private void Update()
    {
        if (hasBeenMinedLux)
        {
            this.gameObject.SetActive(false);
            breakState = OreState.BROKEN;
        }
    }

    public override void GetsMined(int damageAmount, int numberOfDrops)
    {
        StartCoroutine(FlashLightAppears());

        base.GetsMined(damageAmount, 1);

        if(healthSystem.GetHealth() <= 0)
        {
            hasBeenMinedLux = true;
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
