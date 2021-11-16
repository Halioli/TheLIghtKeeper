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

    // Public Attributes
    public GameObject lampLight;
    public Sprite lampSprite;

    private void Awake()
    {
        lampTime = maxLampTime = 5f;
        turnedOn = false;
        lampSpriteRenderer = GetComponent<SpriteRenderer>();
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
            DeactivateLampLight();
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
        //turnedOn = true;
        //lampLight.SetActive(true);
        //lampSpriteRenderer.sprite = lampSprite;
    }

    public void DeactivateLampLight()
    {
        //turnedOn = false;
        //lampLight.SetActive(false);
        //lampSpriteRenderer.sprite = null;
    }

    public float GetLampTimeRemaining()
    {
        return lampTime;
    }
}
