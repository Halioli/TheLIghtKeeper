using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeBroken : InteractStation
{
    private const int IRON_REPAIR_AMOUNT = 12;
    private const int METAL_REPAIR_AMOUNT = 1;

    private ConstuctionPopUp constuctionPopUp;

    [SerializeField] BridgeManager bridgeManager;
    [SerializeField] Item ironMaterial;
    [SerializeField] Item enrichedMetalMaterial;

    private void Start()
    {
        constuctionPopUp = GetComponentInChildren<ConstuctionPopUp>();
    }

    private void Update()
    {
        // If player enters the trigger area the interactionText will appears
        if (playerInsideTriggerArea)
        {
            // Waits the input from interactStation
            GetInput();
            PopUpAppears();
        }
        else
        {
            PopUpDisappears();
        }
    }

    // From InteractStation script
    public override void StationFunction()
    {
        // Check if player has enough items
        bool hasEnoughIron = playerInventory.InventoryContainsItemAndAmount(ironMaterial, IRON_REPAIR_AMOUNT);
        bool hasEnoughMetal = playerInventory.InventoryContainsItemAndAmount(enrichedMetalMaterial, METAL_REPAIR_AMOUNT);

        if (hasEnoughIron && hasEnoughMetal)
        {
            constuctionPopUp.ChangeMessageText("Materials added");
            playerInventory.SubstractNItemsFromInventory(ironMaterial, IRON_REPAIR_AMOUNT);
            playerInventory.SubstractNItemsFromInventory(enrichedMetalMaterial, METAL_REPAIR_AMOUNT);

            constuctionPopUp.SetAllValue(0);
            bridgeManager.BridgeConstructed();
        }
        else
        {
            constuctionPopUp.ChangeMessageText("Not enough materials");

            InvokeOnNotEnoughMaterials();            
        }
    }

    // Interactive pop up disappears
    private void PopUpAppears()
    {
        constuctionPopUp.ShowAll();
    }

    // Interactive pop up disappears
    private void PopUpDisappears()
    {
        constuctionPopUp.HideAll();
        constuctionPopUp.ChangeMessageText("Press E to interact");
    }
}
