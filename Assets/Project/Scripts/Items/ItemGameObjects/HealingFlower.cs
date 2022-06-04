using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFlower : ItemGameObject
{
    [SerializeField] GameObject healingFlowerAuxiliar;

    public delegate void HealingFlowerSound();
    public static event HealingFlowerSound onHealingFlowerUse;


    private void FunctionalitySound()
    {
        if (onHealingFlowerUse != null)
            onHealingFlowerUse();
    }

    public override void DoFunctionality()
    {
        Instantiate(healingFlowerAuxiliar, PlayerInputs.instance.transform);
        FunctionalitySound();
    }
}