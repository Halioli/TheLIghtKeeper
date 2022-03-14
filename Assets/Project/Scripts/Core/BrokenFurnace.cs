using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrokenFurnace : InteractStation
{
    private const int REPAIR_AMOUNT = 6;
    private PopUp popUp;

    public Item coal;
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
        if (playerInventory.InventoryContainsItemAndAmount(coal, REPAIR_AMOUNT))
        {
            popUp.ChangeMessageText("6 coal added");
            playerInventory.SubstractNItemsFromInventory(coal, REPAIR_AMOUNT);
            hud.DoFadeToBlack();

            // Load Scene
            //SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
        }
        else
        {
            popUp.ChangeMessageText("Not enough coal");
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
        popUp.ChangeMessageText("6 coal needed");
    }
}
