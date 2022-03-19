using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeBroken : InteractStation
{
    private const int IRON_REPAIR_AMOUNT = 12;
    private const int METAL_REPAIR_AMOUNT = 1;

    private ConstuctionPopUp constuctionPopUp;

    [SerializeField] Item ironMaterial;
    [SerializeField] Item enrichedMetalMaterial;

    private void Start()
    {
        constuctionPopUp = GetComponentInChildren<ConstuctionPopUp>();
    }
}
