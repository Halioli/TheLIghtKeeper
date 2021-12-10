using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternFuelGameObject : ItemGameObject
{

    private Lamp playerLamp;
    private float lampTimeToRefill = 5f;

    private void Start()
    {
        playerLamp = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Lamp>();
    }
    public override void DoFunctionality()
    {
        if (playerLamp.CanRefill())
        {
            playerLamp.RefillLampTime(lampTimeToRefill);
        }
    }


}
