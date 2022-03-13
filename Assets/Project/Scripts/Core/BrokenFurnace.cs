using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenFurnace : InteractStation
{
    private const int REPAIR_AMOUNT = 6;

    public Item coal;
    public CanvasGroup requirementsCanvasGroup;
    public GameObject furnaceGameObject;
    public Inventory playerInventory;

    private void Start()
    {
        furnaceGameObject.SetActive(false);
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
        if (playerInventory.InventoryContainsItemAndAmount(coal, REPAIR_AMOUNT))
        {
            playerInventory.SubstractNItemsFromInventory(coal, REPAIR_AMOUNT);
            
            furnaceGameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {

        }
    }

    // Interactive pop up disappears
    private void PopUpAppears()
    {
        requirementsCanvasGroup.alpha = 1f;
    }

    // Interactive pop up disappears
    private void PopUpDisappears()
    {
        requirementsCanvasGroup.alpha = 0f;
    }
}
