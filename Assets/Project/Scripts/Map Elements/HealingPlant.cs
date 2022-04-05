using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Experimental.Rendering.Universal;

public class HealingPlant : InteractStation
{
    private Light2D plantLight;
    private Animator plantAnimator;
    private SpriteRenderer spritePlant;
    private bool hasBeenUsed = false;
    private bool inCoroutine = false;
    private float timeStart = 0;
    [SerializeField] private float cooldown;

    public GameObject interactText;
    public GameObject healingFlower;

    // Audio
    [SerializeField] AudioSource healingDroneAudioSource;
    [SerializeField] AudioSource popAndRestartAudioSource;
    [SerializeField] AudioClip popOffAudioClip;
    [SerializeField] AudioClip rechargeAudioClip;


    void Start()
    {
        plantAnimator = GetComponent<Animator>();
        spritePlant = GetComponent<SpriteRenderer>();
        interactText.SetActive(false);
        spritePlant.color = new Color(255, 255, 255, 255);
        plantAnimator.SetBool("used", false);
        plantLight = GetComponentInChildren<Light2D>();
    }

    void Update()
    {
        if (playerInsideTriggerArea && !hasBeenUsed)
        {
            interactText.SetActive(true);
            GetInput();
        }
        else if (!playerInsideTriggerArea)
        {
            interactText.SetActive(false);
        }
        CountDown();

        if (!hasBeenUsed && !inCoroutine)
        {
            StartCoroutine(ElectricPlantFlicker());
        }
    }

    public override void StationFunction()
    {
        if (!hasBeenUsed)
        {
            TurnOffElectricPlant();
        }
    }

    private void CountDown()
    {
        if (hasBeenUsed)
        {
            timeStart -= Time.deltaTime;
            if (timeStart <= 0.0f)
            {
                TurnOnElectricPlant();
            }
        }
    }

    private void TurnOffElectricPlant()
    {
        transform.DOPunchScale(new Vector3(0.4f, -0.4f, 0f), 0.4f, 1);
        plantAnimator.SetBool("used", true);
        timeStart = cooldown;
        hasBeenUsed = true;

        GameObject gameObject = Instantiate(healingFlower, transform);
        gameObject.GetComponent<ItemGameObject>().DropsRandom(true, 1.5f);

        plantLight.intensity = 0.5f;
        plantLight.pointLightOuterRadius = 1.21f;
        plantLight.pointLightInnerRadius = 0.20f;

        interactText.SetActive(false);
        StopCoroutine(ElectricPlantFlicker());

        StopElectricDroneSound();
        PlayPopOffSound();
    }

    private void TurnOnElectricPlant()
    {
        hasBeenUsed = false;
        plantLight.intensity = 1f;
        plantLight.pointLightOuterRadius = 2f;
        plantLight.pointLightInnerRadius = 0.66f;
        plantAnimator.SetBool("used", false);

        if (playerInsideTriggerArea) interactText.SetActive(true);

        PlayElectricDroneSound();
        PlayRechargedSound();
    }


    private void PlayElectricDroneSound()
    {
        healingDroneAudioSource.Play();
    }
    private void StopElectricDroneSound()
    {
        healingDroneAudioSource.Stop();
    }

    private void PlayPopOffSound()
    {
        popAndRestartAudioSource.clip = popOffAudioClip;
        popAndRestartAudioSource.Play();
    }
    private void PlayRechargedSound()
    {
        popAndRestartAudioSource.clip = rechargeAudioClip;
        popAndRestartAudioSource.Play();
    }

    private IEnumerator ElectricPlantFlicker()
    {
        inCoroutine = true;

        plantLight.intensity = Random.Range(0.8f, 1.0f);

        yield return new WaitForSeconds(0.1f);

        inCoroutine = false;
    }
}
