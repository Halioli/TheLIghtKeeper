using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrokenFurnace : InteractStation
{
    private const int COAL_REPAIR_AMOUNT = 6;
    private const int IRON_REPAIR_AMOUNT = 4;
    private PopUp popUp;

    public Item coal;
    public Item iron;
    public HUDHandler hud;

    private void Start()
    {
        popUp = GetComponentInChildren<PopUp>();
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
        if (playerInventory.InventoryContainsItemAndAmount(coal, COAL_REPAIR_AMOUNT) 
            && playerInventory.InventoryContainsItemAndAmount(iron, IRON_REPAIR_AMOUNT))
        {
            popUp.ChangeMessageText("Materials added");
            playerInventory.SubstractNItemsFromInventory(coal, COAL_REPAIR_AMOUNT);
            playerInventory.SubstractNItemsFromInventory(iron, IRON_REPAIR_AMOUNT);
            hud.DoFadeToBlack();

            // Load Scene
            SceneManager.LoadSceneAsync("Spaceship", LoadSceneMode.Single);
        }
        else
        {
            popUp.ChangeMessageText("Not enough materials");
        }
    }

    // Interactive pop up disappears
    private void PopUpAppears()
    {
        popUp.ShowAll();
    }

    // Interactive pop up disappears
    private void PopUpDisappears()
    {
        popUp.HideAll();
        popUp.ChangeMessageText("Press E to interact");
    }
}
