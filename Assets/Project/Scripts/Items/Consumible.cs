using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumible : Item
{
    private void Start()
    {
        isMineral = false;
        isConsumible = true;
    }


    public override void DoFunctionality()
    {
        // Consumible does functionality
    }
}
