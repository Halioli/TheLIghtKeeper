using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    private float maxLampTime;
    private float lampTime;
    private bool turnedOn;

    public GameObject lampLight;

    // Start is called before the first frame update
    private void Start()
    {
        lampTime = maxLampTime = 3f;
        turnedOn = false;
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
        turnedOn = true;
        lampLight.SetActive(true);
    }

    public void DeactivateLampLight()
    {
        turnedOn = false;
        lampLight.SetActive(false);
    }
}
