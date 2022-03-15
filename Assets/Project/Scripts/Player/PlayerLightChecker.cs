using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightChecker : MonoBehaviour
{
    // Private Attributes
    private bool playerInLight;
    public int numberOfLights;

    // Public Attributes
    public Lamp lamp;

    public delegate void PlayerEntersLightAction();
    public static event PlayerEntersLightAction OnPlayerEntersLight;
    public static event PlayerEntersLightAction OnPlayerEntersCoreLight;



    private void Start()
    {
        playerInLight = false;
        numberOfLights = 0;
    }


    private void Update()
    {
        if (numberOfLights == 0)// && !lamp.LampTimeExhausted())
        {
            lamp.UpdateLamp();
        }
        else if (playerInLight && numberOfLights == 0)
        {
            SetPlayerInLightToFalse();
        }
        else if (!playerInLight && numberOfLights > 0)
        {
            SetPlayerInLightToTrue();
        }
    }

    // Method that checks if the player enters an area with light
    private void OnTriggerEnter2D(Collider2D lightingCollider)
    {
        if (lightingCollider.gameObject.CompareTag("Light") || lightingCollider.gameObject.CompareTag("CoreLight"))
        {
            ++numberOfLights;
            
            if (OnPlayerEntersLight != null) OnPlayerEntersLight();

            // Lamp turns off
            if (lamp.active)
                lamp.DeactivateLampLight();

            if (lightingCollider.gameObject.CompareTag("CoreLight"))
            {
                if (!lamp.LampTimeIsMax())
                {
                    if (OnPlayerEntersCoreLight != null) OnPlayerEntersCoreLight();
                    lamp.FullyRefillLampTime();
                }
            }

            SetPlayerInLightToTrue();
        }

    }

    //private void OnTriggerStay2D(Collider2D lightingCollider)
    //{
    //    if (lightingCollider.gameObject.CompareTag("Light") || lightingCollider.gameObject.CompareTag("CoreLight"))
    //    {
    //        // Lamp turns off
            
    //        if (lightingCollider.gameObject.CompareTag("CoreLight"))
    //        {
    //            lamp.FullyRefillLampTime();
    //        }

    //        SetPlayerInLightToTrue();
    //    }

    //}


    // Method that checks if the player exits an area with light
    private void OnTriggerExit2D(Collider2D lightingCollider)
    {
        if (lightingCollider.gameObject.CompareTag("Light") || lightingCollider.gameObject.CompareTag("CoreLight"))
        {
            numberOfLights -= 1;
            if (numberOfLights == 0)
            {
                if (!lamp.LampTimeExhausted())
                {
                    // Lamp turns on
                    lamp.ActivateLampLight();
                }
                else
                {
                    SetPlayerInLightToFalse();
                    lamp.ActivateFadedCircleLight();
                }
            }
        }

    }


    // Method that returns playerInLight bool
    public bool IsPlayerInLight() { return playerInLight; }

    // Method that sets playerInLight bool to true
    public void SetPlayerInLightToTrue() { 
        playerInLight = true;
        lamp.playerInLight = true;
    }

    // Method that sets playerInLight bool to false
    public void SetPlayerInLightToFalse() { 
        playerInLight = false;
        lamp.playerInLight = false;
    }
}
