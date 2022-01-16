using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternFuelGameObject : ItemGameObject
{
    private Lamp playerLamp;
    private float lampTimeToRefill = 5f;

    public delegate void LanternFuelSound();
    public static event LanternFuelSound onLanternFuelRefill;



    private void FunctionalitySound()
    {
        if (onLanternFuelRefill != null)
            onLanternFuelRefill();
    }

    public override void DoFunctionality()
    {
        canBePickedUp = false;
        playerLamp = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Lamp>();

        if (playerLamp.CanRefill())
        {
            FunctionalitySound();
            playerLamp.RefillLampTime(lampTimeToRefill);
        }

        Destroy(gameObject);
    }
}
