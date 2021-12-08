using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Lamp : MonoBehaviour
{
    // Private Attributes
    private const float CONE_LIGHT_INTENSITY_ON = 0.25f;
    private const float CONE_LIGHT_INTENSITY_OFF = 0f;
    private const float CIRCLE_LIGHT_INTENSITY_ON = 1f;
    private const float CIRCLE_LIGHT_INTENSITY_OFF = 0.1f;

    private const float LIGHT_INTENSITY_ON = 0.3f;
    private const float LIGHT_INTENSITY_OFF = 0.0f;


    private float maxLampTime;
    private float lampTime;
    private bool turnedOn;
    private SpriteRenderer lampSpriteRenderer;
    private Inventory playerInventory;

    // Public Attributes
    public GameObject lampCircleLight;
    public GameObject lampConeLight;

    public Animator animator;

    public float flickerIntensity;
    public float flickerTime;

    System.Random rg;

    private void Awake()
    {
        lampTime = maxLampTime = 20f;
        turnedOn = false;
        lampSpriteRenderer = GetComponent<SpriteRenderer>();
        rg = new System.Random();
        flickerTime = 0.08f;
        flickerIntensity = 1f;
    }

    private void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
    }

    private void Update()
    {
        if (turnedOn)
        {
            UpdateLamp();
        }
    }

    public void UpdateLamp()
    {
        if (LampTimeExhausted())
        {
            turnedOn = false;
            animator.SetBool("light", false);
            DeactivateConeLight();
            GetComponentInParent<PlayerLightChecker>().SetPlayerInLightToFalse();
        }
        else
        {
            ConsumeLampTime();
        }
    }

    public bool LampTimeExhausted()
    {
        return lampTime <= 0;
    }

    public void ConsumeLampTime()
    {
        lampTime -= Time.deltaTime;
    }

    public void FullyRefillLampTime()
    {
        lampTime = maxLampTime;
    }

    public void ActivateLampLight()
    {
        turnedOn = true;
        animator.SetBool("light", true);
        
        //lampConeLight.SetActive(true);
        //lampCircleLight.SetActive(true);

        //circlePointLight.intensity = CIRCLE_LIGHT_INTENSITY_ON;
        //coneParametricLight.intensity = CONE_LIGHT_INTENSITY_ON;

        ActivateConeLight();
        ActivateCircleLight();

        StartCoroutine("LightFlicking");
    }

    public void ActivateConeLight()
    {
        lampConeLight.GetComponent<Light2D>().intensity = LIGHT_INTENSITY_ON;
        StartCoroutine("ExpandConeLightOnActivate");
    }
    public void ActivateCircleLight()
    {
        lampCircleLight.GetComponent<Light2D>().intensity = LIGHT_INTENSITY_ON;
    }


    public void DeactivateLampLight()
    {
        turnedOn = false;
        animator.SetBool("light", false);

        //lampConeLight.SetActive(false);
        //lampCircleLight.SetActive(false);
        DeactivateConeLight();
        DeactivateCircleLight();
    }

    public void DeactivateConeLight()
    {
        lampConeLight.GetComponent<Light2D>().intensity = LIGHT_INTENSITY_OFF;
        StartCoroutine("ShrinkConeLightOnActivate");
    }
    public void DeactivateCircleLight()
    {
        lampCircleLight.GetComponent<Light2D>().intensity = LIGHT_INTENSITY_OFF;
    }


    public float GetLampTimeRemaining()
    {
        return lampTime;
    }


    IEnumerator LightFlicking()
    {
        while (turnedOn)
        {
            lampCircleLight.GetComponent<Light2D>().intensity = LIGHT_INTENSITY_ON;
            lampConeLight.GetComponent<Light2D>().intensity = LIGHT_INTENSITY_ON;

            float lightingTime = 5 + ((float)rg.NextDouble() - 0.5f);
            yield return new WaitForSeconds(lightingTime);

            int flickerCount = rg.Next(4, 9);

            for(int i = 0; i < flickerCount; i++)
            {
                float flickingIntensity = 1f - ((float)rg.NextDouble() * flickerIntensity);
                lampCircleLight.GetComponent<Light2D>().intensity = flickingIntensity;
                lampConeLight.GetComponent<Light2D>().intensity = flickingIntensity;

                float flickingTime = (float)rg.NextDouble() * flickerTime;
                if (lampTime < flickingTime)
                {
                    yield return new WaitForSeconds(lampTime - Time.deltaTime);
                    if (!turnedOn) break;
                }
                else
                {
                    yield return new WaitForSeconds(flickingTime);
                }
            }
        }

        ActivateCircleLight();
        DeactivateConeLight();

    }

    IEnumerator ExpandConeLightOnActivate()
    {
        float length = 35f;

        for (float i = 0f; i < length; i += Time.deltaTime * 30f)
        {
            lampConeLight.GetComponent<Light2D>().pointLightOuterRadius = i;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    IEnumerator ShrinkConeLightOnActivate()
    {
        float length = 35f;

        for (float i = length; i >= 0f; i -= Time.deltaTime * 30f)
        {
            lampConeLight.GetComponent<Light2D>().pointLightOuterRadius = i;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

}
