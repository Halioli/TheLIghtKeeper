using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Lamp : MonoBehaviour
{
    // Private Attributes
    private const float LIGHT_ROD_REFUEL_AMOUNT = 10f;
    private const float INNER_OUTER_ANGLE = 259.89f;
    private const float INNER_OUTER_COMPLETE_ANGLE = 360f;
    private const float POINTLIGHT_INNER_RADIUS_OFF = 1f;
    private const float POINTLIGHT_INNER_RADIUS_ON = 3f;
    private const float POINTLIGHT_OUTER_RADIUS_OFF = 2f;
    private const float POINTLIGHT_OUTER_RADIUS_ON = 4f;

    private float maxLampTime;
    private float lampTime;
    private bool turnedOn;
    private SpriteRenderer lampSpriteRenderer;
    private Inventory playerInventory;
    private Light2D pointLight2D;

    // Public Attributes
    public GameObject lampLight;
    public GameObject lampSpriteObject;
    public Sprite lampSprite;
    public Item lightRodItem;
    public GameObject coneLight;

    private void Awake()
    {
        lampTime = maxLampTime = 20f;
        turnedOn = false;
        lampSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
        pointLight2D = GetComponentsInChildren<Light2D>()[0];
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
            CheckPlayerInventoryForLightRods();
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
        lampLight.SetActive(true);
        coneLight.SetActive(true);
        pointLight2D.pointLightInnerAngle = INNER_OUTER_ANGLE;
        pointLight2D.pointLightOuterAngle = INNER_OUTER_ANGLE;
        pointLight2D.pointLightInnerRadius = POINTLIGHT_INNER_RADIUS_ON;
        pointLight2D.pointLightOuterRadius = POINTLIGHT_OUTER_RADIUS_ON;
        pointLight2D.intensity = 1f;

        lampSpriteObject.GetComponent<SpriteRenderer>().sprite = lampSprite;
    }

    public void DeactivateLampLight()
    {
        turnedOn = false;
        lampLight.SetActive(false);
        lampSpriteObject.GetComponent<SpriteRenderer>().sprite = null;
    }

    public void DeactivateConeLightButNotPointLight()
    {
        coneLight.SetActive(false);
        pointLight2D.pointLightInnerAngle = INNER_OUTER_COMPLETE_ANGLE;
        pointLight2D.pointLightOuterAngle = INNER_OUTER_COMPLETE_ANGLE;
        pointLight2D.pointLightInnerRadius = POINTLIGHT_INNER_RADIUS_OFF;
        pointLight2D.pointLightOuterRadius = POINTLIGHT_OUTER_RADIUS_OFF;
        pointLight2D.intensity = 0.1f;
    }

    public float GetLampTimeRemaining()
    {
        return lampTime;
    }

   
    private void CheckPlayerInventoryForLightRods()
    {
        if (playerInventory.InventoryContainsItem(lightRodItem))
        {
            playerInventory.SubstractItemToInventory(lightRodItem);
            lampTime += LIGHT_ROD_REFUEL_AMOUNT;
            GetComponentInParent<PlayerLightChecker>().SetPlayerInLightToTrue();
        }
        else
        {
            DeactivateConeLightButNotPointLight();
            GetComponentInParent<PlayerLightChecker>().SetPlayerInLightToFalse();
        }
    }
}
