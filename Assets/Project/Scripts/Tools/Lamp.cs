using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Lamp : MonoBehaviour
{
    // Private Attributes

    private const float POINTLIGHT_INNER_RADIUS_OFF = 1f;
    private const float POINTLIGHT_INNER_RADIUS_ON = 6f;
    private const float POINTLIGHT_OUTER_RADIUS_OFF = 2f;
    private const float POINTLIGHT_OUTER_RADIUS_ON = 8f;

    private float maxLampTime;
    private float lampTime;
    private bool turnedOn;
    private SpriteRenderer lampSpriteRenderer;
    private Inventory playerInventory;

    public float flickerIntensity = 1f;
    public float flickerTime = 0.08f;

    // Public Attributes
    public GameObject lampLightCone;
    public Light2D pointLight;

    public Animator animator;

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
        //pointLight2D.pointLightInnerRadius = POINTLIGHT_INNER_RADIUS_ON;
        //pointLight2D.pointLightOuterRadius = POINTLIGHT_OUTER_RADIUS_ON;
        pointLight.intensity = 1f;
    }

    public void DeactivateLampLight()
    {
        turnedOn = false;
        animator.SetBool("light", false);
        lampLightCone.SetActive(false);
    }

    public void DeactivateConeLightButNotPointLight()
    {
        //pointLight2D.pointLightInnerRadius = POINTLIGHT_INNER_RADIUS_OFF;
        //pointLight2D.pointLightOuterRadius = POINTLIGHT_OUTER_RADIUS_OFF;
        pointLight.intensity = 0.1f;
    }

    public float GetLampTimeRemaining()
    {
        return lampTime;
    }


    IEnumerator Flicker()
    {
        while (true)
        {
            pointLight.intensity = 1f;

            float lightingTime = 5 + ((float)rg.NextDouble() - 0.5f);
            yield return new WaitForSeconds(lightingTime);

            int flickerCount = rg.Next(4, 9);

            for(int i = 0; i < flickerCount; i++)
            {
                float flickingIntensity = 1f - ((float)rg.NextDouble() * flickerIntensity);
                pointLight.intensity = flickingIntensity;

                float flickingTime = (float)rg.NextDouble() * flickerTime;
                yield return new WaitForSeconds(flickingTime);
            }
        }
    }
}
