using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    // Private Attributes
    private float maxLampTime;
    private float lampTime;
    private bool turnedOn;
    private SpriteRenderer lampSpriteRenderer;
    private Inventory playerInventory;

    // Public Attributes
    public GameObject lampLight;
    public GameObject lampSpriteObject;
    public Sprite lampSprite;
    public Item lightRodItem;

    private void Awake()
    {
        lampTime = maxLampTime = 20f;
        turnedOn = false;
        lampSpriteRenderer = GetComponent<SpriteRenderer>();
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
        lampSpriteObject.GetComponent<SpriteRenderer>().sprite = lampSprite;
    }

    public void DeactivateLampLight()
    {
        turnedOn = false;
        lampLight.SetActive(false);
        lampSpriteObject.GetComponent<SpriteRenderer>().sprite = null;
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
            FullyRefillLampTime();
        }
        else
        {
            DeactivateLampLight();
        }
    }
}
