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

    private const float LIGHT_INTENSITY_ON = 0.5f;
    private const float LIGHT_INTENSITY_OFF = 0.0f;

    private const float RADIUS_DIFFERENCE = 20f;

    private float[] LIGHT_ANGLE_LVL = { 35f, 55f, 75f, 95f, 115f };
    private float[] LIGHT_DISTANCE_LVL = { 5f, 10f, 15f, 20f, 25f };
    private float lightAngle;
    private float lightDistance;

    private const float LIGHT_CIRCLE_OUTER_RADIUS = 2f;
    private const float LIGHT_CIRCLE_INNER_RADIUS = 1.5f;
    private const float LIGHT_CIRCLE_RADIUS_DIFFERENCE = LIGHT_CIRCLE_OUTER_RADIUS - LIGHT_CIRCLE_INNER_RADIUS;

    private bool coneIsActive = false;

    private float maxLampTime;
    private float lampTime;
    private bool turnedOn;
    private SpriteRenderer lampSpriteRenderer;
    private Inventory playerInventory;

    // Public Attributes
    public bool turnedOn;
    public float lampTime;
    public bool active = false;
    public bool canRefill;

    public Animator animator;

    public float flickerIntensity;
    public float flickerTime;

    System.Random rg;


    public delegate void PlayLanternSound();
    public static event PlayLanternSound turnOnLanternSoundEvent;
    public static event PlayLanternSound turnOffLanternSoundEvent;
    public static event PlayLanternSound turnOnLanternDroneSoundEvent;
    public static event PlayLanternSound turnOffLanternDroneSoundEvent;
    public static event PlayLanternSound playLanternDroneSoundEvent;
    public static event PlayLanternSound stopLanternDroneSoundEvent;



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
        StartCoroutine(Flicker());
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
            DeactivateConeLightButNotPointLight();
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

    public void RefillLampTime(float time)
    {
        if (lampTime + time > maxLampTime)
        {
            FullyRefillLampTime();
        }
        else
        {
            lampTime += time;
        }
    }

    public bool CanRefill()
    {
        if (turnedOn)  //Player in darkness
        {
            if (lampTime == 0 || lampTime == maxLampTime) //Player don't has lamp fuel
            {
                return false;
            }
            else //Player has lamp fuel
            {
                return true;
            }
        }
        else //Player in light
        {
            if(lampTime != maxLampTime) //Player has no max lamp fuel
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void ActivateLampLight()
    {
        turnedOn = true;
        animator.SetBool("light", true);
        lampLightCone.SetActive(true);
        lampLightCircle.SetActive(true);

        ActivateConeLight();
        ActivateCircleLight();

        StartCoroutine("LightFlicking");

        if (turnOnLanternSoundEvent != null)
            turnOnLanternSoundEvent();
    }

    public void ActivateConeLight()
    {
        coneIsActive = true;

        lampConeLight.SetActive(true);

        lampConeLight.GetComponent<Light2D>().intensity = LIGHT_INTENSITY_ON;
        StartCoroutine("ExpandConeLight");


        if (turnOnLanternDroneSoundEvent != null)
            turnOnLanternDroneSoundEvent();
    }
    public void ActivateCircleLight()
    {
        lampCircleLight.SetActive(true);

        active = true;

        lampCircleLight.GetComponent<Light2D>().intensity = LIGHT_INTENSITY_ON;
        StartCoroutine("ExpandCircleLight");

        if (playLanternDroneSoundEvent != null)
            playLanternDroneSoundEvent();
    }


    public void DeactivateLampLight()
    {
        turnedOn = false;
        animator.SetBool("light", false);

        if (coneIsActive)
        {
            DeactivateConeLight();
        }
        
        DeactivateCircleLight();

        if (turnOffLanternSoundEvent != null)
            turnOffLanternSoundEvent();
    }

    public void DeactivateConeLightButNotPointLight()
    {
        coneIsActive = false;

        StopCoroutine("LightFlicking");

        StartCoroutine("ShrinkConeLight");

        if (turnOffLanternDroneSoundEvent != null)
            turnOffLanternDroneSoundEvent();
    }
    public void DeactivateCircleLight()
    {
        active = false;

        StartCoroutine("ShrinkCircleLight");

        if (stopLanternDroneSoundEvent != null)
            stopLanternDroneSoundEvent();
    }

    public float GetLampTimeRemaining()
    {
        return lampTime;
    }


    IEnumerator Flicker()
    {
        while (true)
        {
            circlePointLight.intensity = CIRCLE_LIGHT_INTENSITY_ON;
            coneParametricLight.intensity = CONE_LIGHT_INTENSITY_ON;

            float lightingTime = 5 + ((float)rg.NextDouble() - 0.5f);
            yield return new WaitForSeconds(lightingTime);

            int flickerCount = rg.Next(4, 9);

            for (int i = 0; i < flickerCount; i++)
            {
                float flickingIntensity = 1f - ((float)rg.NextDouble() * flickerIntensity);
                circlePointLight.intensity = flickingIntensity;
                coneParametricLight.intensity = flickingIntensity;

                float flickingTime = (float)rg.NextDouble() * flickerTime;
                yield return new WaitForSeconds(flickingTime);
            }
        }


        DeactivateConeLight();

    }

    IEnumerator ExpandConeLight()
    {
        StopCoroutine("ShrinkConeLight");

        for (float i = 0f; i < lightAngle; i += Time.deltaTime * lightAngle * 4)
        {
            lampConeLight.GetComponent<Light2D>().pointLightOuterAngle = i;
            lampConeLight.GetComponent<Light2D>().pointLightInnerAngle = (i >= RADIUS_DIFFERENCE ? i - RADIUS_DIFFERENCE : 0f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    IEnumerator ShrinkConeLight()
    {
        StopCoroutine("ExpandConeLight");

        for (float i = lightAngle; i > 0f; i -= Time.deltaTime * lightAngle * 8)
        {
            lampConeLight.GetComponent<Light2D>().pointLightOuterAngle = i;
            lampConeLight.GetComponent<Light2D>().pointLightInnerAngle = (i <= RADIUS_DIFFERENCE ? 0f : i - RADIUS_DIFFERENCE);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        lampConeLight.SetActive(false);
    }


    IEnumerator ExpandCircleLight()
    {
        for (float i = 0f; i < LIGHT_CIRCLE_OUTER_RADIUS; i += Time.deltaTime * LIGHT_CIRCLE_OUTER_RADIUS * 8)
        {
            lampCircleLight.GetComponent<Light2D>().pointLightOuterRadius = i;
            lampCircleLight.GetComponent<Light2D>().pointLightInnerRadius = i > LIGHT_CIRCLE_RADIUS_DIFFERENCE ? i - LIGHT_CIRCLE_RADIUS_DIFFERENCE : 0f;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    IEnumerator ShrinkCircleLight()
    {
        for (float i = LIGHT_CIRCLE_OUTER_RADIUS; i > 0f; i -= Time.deltaTime * LIGHT_CIRCLE_OUTER_RADIUS * 8)
        {
            lampCircleLight.GetComponent<Light2D>().pointLightOuterRadius = i;
            lampCircleLight.GetComponent<Light2D>().pointLightInnerRadius = i > LIGHT_CIRCLE_RADIUS_DIFFERENCE ? i - LIGHT_CIRCLE_RADIUS_DIFFERENCE : 0f;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        lampCircleLight.GetComponent<Light2D>().intensity = LIGHT_INTENSITY_OFF;

        lampCircleLight.SetActive(false);
    }

}
