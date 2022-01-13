using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternFuelGameObject : ItemGameObject
{
    private Lamp playerLamp;
    private float lampTimeToRefill = 5f;

    public override void DoFunctionality()
    {
        canBePickedUp = false;
        playerLamp = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Lamp>();

        if (playerLamp.CanRefill())
        {
            playerLamp.RefillLampTime(lampTimeToRefill);
        }

        Destroy(gameObject);
    }
}
