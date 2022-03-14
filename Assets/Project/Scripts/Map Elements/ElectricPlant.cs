using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Experimental.Rendering.Universal;

public class ElectricPlant : InteractStation
{
    private GameObject player;
    private Lamp playerLamp;

    private Light2D plantLight;
    private Animator plantAnimator;
    private SpriteRenderer spritePlant;
    public GameObject interactText;

    public GameObject electricOrb;

    private bool hasBeenUsed = false;
    private float timeStart = 0;

    private Vector3 positionSpawnOrb = new Vector3(1, 1, 0);

    // Start is called before the first frame update
    void Start()
    {
        plantAnimator = GetComponent<Animator>();
        spritePlant = GetComponent<SpriteRenderer>();
        interactText.SetActive(false);
        spritePlant.color = new Color(255, 255, 255, 255);
        player = GameObject.FindGameObjectWithTag("Player");
        playerLamp = player.GetComponentInChildren<Lamp>();
        plantAnimator.SetBool("used", false);
        plantLight = GetComponentInChildren<Light2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInsideTriggerArea)
        {
            interactText.SetActive(true);
            GetInput();
        }
        else
        {
            interactText.SetActive(false);
        }
        CountDown();
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
        transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0f), 0.2f);
        plantAnimator.SetBool("used", true);
        timeStart = 5f;
        hasBeenUsed = true;

        GameObject gameObject = Instantiate(electricOrb, transform);
        gameObject.GetComponent<ItemGameObject>().DropsRandom();
        gameObject.GetComponent<ItemGameObject>().MakeNotPickupableForDuration();

        //playerLamp.lampTime = playerLamp.GetMaxLampTime();
        plantLight.intensity = 0.5f;
        plantLight.pointLightOuterRadius = 1.21f;
        plantLight.pointLightInnerRadius = 0.20f;

    }

    private void TurnOnElectricPlant()
    {
        hasBeenUsed = false;
        plantLight.intensity = 1f;
        plantLight.pointLightOuterRadius = 1.84f;
        plantLight.pointLightInnerRadius = 0.43f;
        plantAnimator.SetBool("used", false);
    }
}
