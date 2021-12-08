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

    private float maxLampTime;
    private float lampTime;
    private bool turnedOn;
    private SpriteRenderer lampSpriteRenderer;
    private Inventory playerInventory;

    // Public Attributes
    public GameObject lampLightCircle;
    public GameObject lampLightCone;
    public Light2D circlePointLight;
    public Light2D coneParametricLight;

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

    public void ActivateLampLight()
    {
        turnedOn = true;
        animator.SetBool("light", true);
        lampLightCone.SetActive(true);
        lampLightCircle.SetActive(true);

        circlePointLight.intensity = CIRCLE_LIGHT_INTENSITY_ON;
        coneParametricLight.intensity = CONE_LIGHT_INTENSITY_ON;
    }

    public void DeactivateLampLight()
    {
        turnedOn = false;
        animator.SetBool("light", false);
        lampLightCone.SetActive(false);
        lampLightCircle.SetActive(false);
    }

    public void DeactivateConeLightButNotPointLight()
    {
        circlePointLight.intensity = CIRCLE_LIGHT_INTENSITY_OFF;
        coneParametricLight.intensity = CONE_LIGHT_INTENSITY_OFF;
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

            for(int i = 0; i < flickerCount; i++)
            {
                float flickingIntensity = 1f - ((float)rg.NextDouble() * flickerIntensity);
                circlePointLight.intensity = flickingIntensity;
                coneParametricLight.intensity = flickingIntensity;

                float flickingTime = (float)rg.NextDouble() * flickerTime;
                yield return new WaitForSeconds(flickingTime);
            }
        }
    }
}
