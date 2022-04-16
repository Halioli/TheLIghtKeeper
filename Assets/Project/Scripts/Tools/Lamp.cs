using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Lamp : MonoBehaviour
{
    // Private Attributes
    private const float LIGHT_INTENSITY_ON = 1f;
    private const float LIGHT_INTENSITY_OFF = 0.3f;
    private const float LIGHT_INTENSITY_HIGH = 2f;

    private const int MAX_SOURCE_LEVELS = 6;
    private int sourceLevel = 0;
    private float[] LIGHT_ANGLE_LVL = { 50f, 60f, 70f, 80f, 90f };
    private float[] LIGHT_DISTANCE_LVL = { 10f, 12.5f, 15f, 20f, 25f };
    private float lightAngle;
    private float lightDistance;

    private const int MAX_TIME_LEVELS = 3;
    private int timeLevel = 0;
    public float lampTime;
    private float[] LAMP_TIME_LVL = { 5f, 5f, 10f };

    public bool coneIsActive = false;
    public bool intenseCircleIsActive = false;

    private float maxLampTime;
    private Animator playerAnimator;

    // Public Attributes
    public bool playerInLight;
    public bool turnedOn;
    public bool active = false;
    public bool canRefill;

    [SerializeField] private CircleLight circleLight;
    [SerializeField] private ConeLight coneLight;

    public float flickerIntensity;
    public float flickerTime;
    private const float START_FLICK_COOLDOWN = 5f;
    private float flickCooldown = START_FLICK_COOLDOWN;
    private float lowLightflickCooldown = 0.75f;
    private const float SECONDS_HIGH_FREQUENCY_FLICK = 10f;
    private float lampTimeParticles;
    System.Random rg;


    private bool isLightIntense;
    private float timeMultiplier;
    private readonly float normalLightTimeMultiplier = 1f;
    private float intenseLightTimeMultiplier = 3f;

    public ParticleSystem lightRechargedParticleSystem;


    public delegate void PlayLanternSound();
    public static event PlayLanternSound turnOnLanternEvent;
    public static event PlayLanternSound turnOffLanternEvent;
    public static event PlayLanternSound turnOnLanternDroneSoundEvent;
    public static event PlayLanternSound turnOffLanternDroneSoundEvent;

    public static event PlayLanternSound OnLanternTurnsOnInDarkness;

    private void Awake()
    {
        lampTime = maxLampTime = 20f;
        turnedOn = false;
        rg = new System.Random();
        flickerTime = 0.08f;
        flickerIntensity = 1f;

        lightAngle = LIGHT_ANGLE_LVL[sourceLevel];
        lightDistance = LIGHT_DISTANCE_LVL[sourceLevel];

        timeMultiplier = normalLightTimeMultiplier;
        lampTimeParticles = 1f;
    }

    private void Start()
    {
        playerAnimator = GetComponentInParent<Animator>();

        circleLight.SetDistance(3f);
        coneLight.SetDistance(lightDistance);
        coneLight.SetAngle(lightAngle);

        lightRechargedParticleSystem.Stop();
    }

    private void OnEnable()
    {
        DarknessSystem.OnPlayerEntersLight += DeactivateConeLight; //DeactivateLampLight

        LanternSourceUpgrade.OnLanternSourceUpgrade += UpgradeLampSource;
        LanternTimeUpgrade.OnLanternTimeUpgrade += UpgradeLampTime;

        LanternAttack.OnLanternAttackStart += SetTimeMultiplierToIntenseLight;
        LanternAttack.OnLanternAttackEnd += SetTimeMultiplierToNormalLight;
    }

    private void OnDisable()
    {
        DarknessSystem.OnPlayerEntersLight -= DeactivateConeLight; //DeactivateLampLight

        LanternSourceUpgrade.OnLanternSourceUpgrade -= UpgradeLampSource;
        LanternTimeUpgrade.OnLanternTimeUpgrade -= UpgradeLampTime;

        LanternAttack.OnLanternAttackStart -= SetTimeMultiplierToIntenseLight;
        LanternAttack.OnLanternAttackEnd -= SetTimeMultiplierToNormalLight;
    }

    public void UpdateLamp()
    {
        if (LampTimeExhausted())
        {
            turnedOn = false;
            playerAnimator.SetBool("light", false);

            DeactivateConeLight();

            GetComponentInParent<PlayerLightChecker>().SetPlayerInLightToFalse();
            flickCooldown = START_FLICK_COOLDOWN;
            circleLight.SetIntensity(LIGHT_INTENSITY_OFF);

            if (turnOffLanternEvent != null) turnOffLanternEvent();
        }
        else if (!PlayerInputs.instance.isLanternPaused)
        {
            ConsumeLampTime();
            if (lampTime <= SECONDS_HIGH_FREQUENCY_FLICK)
            {
                flickCooldown = lowLightflickCooldown;
            }
        }
    }

    public bool LampTimeExhausted()
    {
        return lampTime <= 0;
    }

    public bool LampTimeIsMax()
    {
        return lampTime == maxLampTime;
    }

    public void ConsumeLampTime()
    {
        lampTime -= Time.deltaTime * timeMultiplier;
    }

    public void ConsumeLampTime(float timeToSubstract)
    {
        lampTime -= timeToSubstract;
    }

    public void FullyRefillLampTime()
    {
        if (LampTimeIsMax()) return;

        lampTime = maxLampTime;
        StartCoroutine("RechargeLampTimeParticles");

    }

    public void RefillLampTime(float time)
    {
        if (LampTimeIsMax()) return;

        if (lampTime + time > maxLampTime)
        {
            FullyRefillLampTime();
        }
        else
        {
            lampTime += time;
        }
        flickCooldown = START_FLICK_COOLDOWN;
        StartCoroutine("RechargeLampTimeParticles");

        if (!turnedOn && !playerInLight)
        {
            // ActivateLampLight(); // <<<<<<<<<<<<<<<<<<<<
            ActivateConeLight(); // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

            if (OnLanternTurnsOnInDarkness != null) OnLanternTurnsOnInDarkness();
        }
    }

    public void ConsumeSpecificLampTime(float time)
    {
        if (lampTime - time < 0)
        {
            lampTime = 0;
        }
        else
        {
            lampTime -= time;
        }
    }

    public bool CanRefill()
    {
        if (turnedOn)  //Player in darkness
        {
            if (lampTime == 0 || lampTime == maxLampTime) //Player doesn't has lamp fuel
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

    //public void ActivateLampLight()
    //{
    //    turnedOn = true;
    //    playerAnimator.SetBool("light", true);

    //    if (!active && turnOnLanternEvent != null)
    //        turnOnLanternEvent();

    //    if (!coneIsActive)
    //        ActivateConeLight();
    //    if (!active)
    //        ActivateCircleLight();

    //    // StartCoroutine("LightFlicking");
    //}


    public void ActivateConeLight()
    {
        if (!active) ActivateCircleLight();


        turnedOn = true;
        playerAnimator.SetBool("light", true);

        if (!coneIsActive && turnOnLanternDroneSoundEvent != null)
            turnOnLanternDroneSoundEvent();

        coneIsActive = true;

        coneLight.SetIntensity(LIGHT_INTENSITY_ON);
        coneLight.Expand(LIGHT_INTENSITY_ON);

        StartCoroutine("LightFlicking");
    }


    public void ActivateCircleLight()
    {
        active = true;
        //intenseCircleIsActive = true;
        intenseCircleIsActive = false;

        circleLight.SetIntensity(LIGHT_INTENSITY_ON);
        circleLight.Expand(LIGHT_INTENSITY_ON);

        if (turnOnLanternEvent != null) turnOnLanternEvent();
    }


    //public void DeactivateLampLight()
    //{
    //    if (turnedOn && turnOffLanternEvent != null)
    //        turnOffLanternEvent();

    //    turnedOn = false;
    //    playerAnimator.SetBool("light", false);

    //    if (coneIsActive)
    //        DeactivateConeLight();

    //    if (active)
    //        DeactivateCircleLight();
    //}



    public void DeactivateConeLight()
    {
        turnedOn = false;
        playerAnimator.SetBool("light", false);


        if (coneIsActive && turnOffLanternDroneSoundEvent != null)
            turnOffLanternDroneSoundEvent();

        coneIsActive = false;

        StopCoroutine("LightFlicking");

        //circleLight.SetIntensity(LIGHT_INTENSITY_OFF);
        coneLight.Shrink(LIGHT_INTENSITY_OFF);

    }

    public void DeactivateCircleLight()
    {
        if (!active) return;

        active = false;
        intenseCircleIsActive = false;

        circleLight.Shrink(LIGHT_INTENSITY_OFF);

        if (turnOffLanternEvent != null) turnOffLanternEvent();
    }

    public float GetLampTimeRemaining()
    {
        return lampTime;
    }

    public float GetMaxLampTime()
    {
        return maxLampTime;
    }

    public void IncrementLightAngleAndDistance(float lightAngleIncrement, float lightDistanceIncrement, float lightIntensity = LIGHT_INTENSITY_HIGH)
    {
        coneLight.SetDistance(lightDistance + lightDistanceIncrement);
        coneLight.ExtraExpand(lightAngle, lightAngle+lightAngleIncrement, lightIntensity);

        circleLight.IntensityFadeInTransition(lightIntensity);
    }

    public void ResetLightAngleAndDistance(float lightAngleIncrement, float lightDistanceIncrement)
    {
        float lightIntensity = turnedOn ? LIGHT_INTENSITY_ON : LIGHT_INTENSITY_OFF;

        coneLight.SetDistance(lightDistance);
        coneLight.PartialShrink(lightAngle + lightAngleIncrement, lightAngle, lightIntensity);

        circleLight.IntensityFadeOutTransition(lightIntensity);
    }

    private void UpgradeLampSource()
    {
        if (sourceLevel >= MAX_SOURCE_LEVELS)
        {
            return;
        }
        if (sourceLevel >= LIGHT_ANGLE_LVL.Length) sourceLevel = LIGHT_ANGLE_LVL.Length;

        lightAngle = LIGHT_ANGLE_LVL[sourceLevel];
        lightDistance = LIGHT_DISTANCE_LVL[sourceLevel];

        coneLight.SetDistance(lightDistance);   
        coneLight.SetAngle(lightAngle);

        ++sourceLevel;
    }

    private void UpgradeLampTime()
    {
        if (timeLevel >= MAX_TIME_LEVELS)
        {
            return;
        }

        maxLampTime += LAMP_TIME_LVL[timeLevel];
        lampTime = maxLampTime;
        ++timeLevel;
    }

    IEnumerator LightFlicking()
    {
        if (lampTime > SECONDS_HIGH_FREQUENCY_FLICK)
        {
            flickCooldown = START_FLICK_COOLDOWN;
        }

        float lightingTime;
        int flickerCount;
        float flickingIntensity;
        float flickingTime;

        while (turnedOn)
        {
            if (isLightIntense)
            {
                circleLight.SetIntensity(LIGHT_INTENSITY_HIGH);
                coneLight.SetIntensity(LIGHT_INTENSITY_HIGH);
            }
            else
            {
                circleLight.SetIntensity(LIGHT_INTENSITY_ON);
                coneLight.SetIntensity(LIGHT_INTENSITY_ON);
            }
            

            lightingTime = flickCooldown + ((float)rg.NextDouble() - 0.5f);
            yield return new WaitForSeconds(lightingTime);

            flickerCount = rg.Next(4, 9);
            
            for (int i = 0; i < flickerCount; ++i)
            {
                flickingIntensity = 1f - ((float)rg.NextDouble() * flickerIntensity);
                circleLight.SetIntensity(flickingIntensity);
                coneLight.SetIntensity(flickingIntensity);

                flickingTime = (float)rg.NextDouble() * flickerTime;
                if (lampTime < SECONDS_HIGH_FREQUENCY_FLICK)
                {
                    if (!turnedOn) break;
                    yield return new WaitForSeconds(flickingTime);
                }
                else
                {
                    yield return new WaitForSeconds(flickingTime);
                }
            }

        }

        //DeactivateConeLight();
    }

    IEnumerator RechargeLampTimeParticles()
    {
        lightRechargedParticleSystem.Play();
        yield return new WaitForSeconds(lampTimeParticles);
        lightRechargedParticleSystem.Stop();
    }


    private void SetTimeMultiplierToNormalLight()
    {
        timeMultiplier = normalLightTimeMultiplier;
        isLightIntense = false;
    }

    private void SetTimeMultiplierToIntenseLight()
    {
        timeMultiplier = intenseLightTimeMultiplier;
        isLightIntense = true;
    }


}
