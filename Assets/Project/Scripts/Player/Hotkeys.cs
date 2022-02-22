using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotkeys : MonoBehaviour
{
    private HealthSystem playerHealthSystem;
    private Lamp playerLamp;

    public GameObject coalMineral;
    public GameObject ironMineral;
    public GameObject darkEssenceMineral;
    public GameObject luxuniteMineral;

    void Start()
    {
        playerHealthSystem = GetComponent<HealthSystem>();
        playerLamp = GetComponentInChildren<Lamp>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            // Spawn 1 iron at the player's feet
            Instantiate(ironMineral, transform);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            // Spawn 1 coal at the player's feet
            Instantiate(coalMineral, transform);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            // Spawn 1 dark essence at the player's feet
            Instantiate(darkEssenceMineral, transform);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            // Spawn 1 luxinite at the player's feet
            Instantiate(luxuniteMineral, transform);
        }
        else if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            // +5 health
            playerHealthSystem.ReceiveHealth(5);
        }
        else if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            // -5 health
            playerHealthSystem.ReceiveDamage(5);
        }
        else if (Input.GetKeyDown(KeyCode.Period))
        {
            // +5 lantern
            playerLamp.RefillLampTime(5f);
        }
        else if (Input.GetKeyDown(KeyCode.Comma))
        {
            // -5 lantern
            playerLamp.ConsumeSpecificLampTime(5f);
        }
    }
}