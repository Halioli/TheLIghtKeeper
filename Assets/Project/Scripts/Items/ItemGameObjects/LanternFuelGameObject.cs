using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LanternFuelGameObject : ItemGameObject
{
    [SerializeField] GameObject lanternFuelAuxiliar;

    public delegate void LanternFuelSound();
    public static event LanternFuelSound onLanternFuelRefill;


    private void FunctionalitySound()
    {
        if (onLanternFuelRefill != null)
            onLanternFuelRefill();
    }

    public override void DoFunctionality()
    {
        Instantiate(lanternFuelAuxiliar, PlayerInputs.instance.transform);
        FunctionalitySound();
    }


}
