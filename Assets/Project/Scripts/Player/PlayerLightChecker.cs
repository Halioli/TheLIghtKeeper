using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightChecker : MonoBehaviour
{
    // Private Attributes
    public static bool playerInLight;
    private bool wasPlayerInDarknessNoLantern = false;
    public static bool playerInDarknessNoLantern;
    public int numberOfLights;

    // Public Attributes
    public Lamp lamp;

    public delegate void PlayerEntersLightAction();
    public static event PlayerEntersLightAction OnPlayerEntersLight; // Invoked when player enters a "Light" or "CoreLight" tagged collider
    public static event PlayerEntersLightAction OnPlayerEntersCoreLight; // Invoked when player "CoreLight" tagged collider
    public static event PlayerEntersLightAction OnPlayerInDarknessNoLantern; // Invoked when player runs out of Lantern light while in the dark

    // Invoked when player enters "LightInterior" tagged collider (already inside "Light" or "CoreLight" tagged collider)
    public static event PlayerEntersLightAction OnPlayerEntersLightInterior; 
    // Invoked when player exits "LightInterior" tagged collider and inside "Light" or "CoreLight" tagged collider
    public static event PlayerEntersLightAction OnPlayerExitsLightInterior;




    private void Start()
    {
        numberOfLights = 0;
    }


    private void Update()
    {
        playerInLight = numberOfLights > 0;

        playerInDarknessNoLantern = lamp.LampTimeExhausted() && !playerInLight;
        if (playerInDarknessNoLantern && !wasPlayerInDarknessNoLantern)
        {
            wasPlayerInDarknessNoLantern = true;
            if (OnPlayerInDarknessNoLantern != null) OnPlayerInDarknessNoLantern();
        }
        else if (!playerInDarknessNoLantern)
        {
            wasPlayerInDarknessNoLantern = false;
        }


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
        if (PlayerInputs.instance.ignoreLights) return;


        if (lightingCollider.gameObject.CompareTag("Light") || lightingCollider.gameObject.CompareTag("CoreLight"))
        {
            ++numberOfLights;
            
            if (OnPlayerEntersLight != null) OnPlayerEntersLight();

            // Lamp turns off
            if (lamp.active)
            {
                // lamp.DeactivateLampLight(); <<<<<<<<<<<<<<<<<<<<
                lamp.DeactivateConeLight();
            }
                

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


        if (lightingCollider.gameObject.CompareTag("LightInterior"))
        {
            if (OnPlayerEntersLightInterior != null) OnPlayerEntersLightInterior();

            lamp.DeactivateCircleLight(); //>>>>>>>>>>>>>>>>>>
        }

    }



    private void OnTriggerExit2D(Collider2D lightingCollider)
    {
        if (PlayerInputs.instance.ignoreLights) return;

        if (lightingCollider.gameObject.CompareTag("Light") || lightingCollider.gameObject.CompareTag("CoreLight"))
        {
            --numberOfLights;
            if (numberOfLights == 0)
            {
                if (lamp.LampTimeExhausted())
                {
                    SetPlayerInLightToFalse();
                    // lamp.ActivateFadedCircleLight(); <<<<<<<<<<<<<<<<<<
                }
                else
                {
                    // Lamp turns on
                    // lamp.ActivateLampLight(); <<<<<<<<<<<<<<<<<<
                    lamp.ActivateConeLight(); // >>>>>>>>>>>>>>>>>>>>>>>
                }
            }
        }


        if (lightingCollider.gameObject.CompareTag("LightInterior") && numberOfLights <= 1)
        {
            if (OnPlayerExitsLightInterior != null) OnPlayerExitsLightInterior();

            lamp.ActivateCircleLight(); //>>>>>>>>>>>>>>>>>>
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
