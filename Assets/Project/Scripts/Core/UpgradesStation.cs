using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesStation : InteractStation
{
    public GameObject interactText;
    public GameObject upgradesCanvasGameObject;
    public GameObject hudGameObject;
    public GameObject inventoryCanvasGameObject;
    public UpgradesSystem upgradesSystem;

    private InventoryMenu inventoryMenu;

    void Start()
    {
        inventoryMenu = inventoryCanvasGameObject.GetComponentInChildren<InventoryMenu>();

        upgradesSystem = GetComponent<UpgradesSystem>();
        upgradesSystem.Init(playerInventory);
        InitUpgradesMenu();
    }

    private void Update()
    {
        if (playerInsideTriggerArea)
        {
            GetInput();            //Waits the input from interactStation 
            PopUpAppears();
        }
        else
        {
            PopUpDisappears();
            if (upgradesCanvasGameObject.activeInHierarchy)
            {
                hudGameObject.SetActive(true);
                upgradesCanvasGameObject.SetActive(false);
            }
        }
    }

    public override void StationFunction()
    {
        // Open menu
        if (!upgradesCanvasGameObject.activeInHierarchy)
        {
            hudGameObject.SetActive(false);
            upgradesCanvasGameObject.SetActive(true);
            inventoryMenu.UpdateInventory();
            PauseMenu.gameIsPaused = true;
        }
        else
        {
            hudGameObject.SetActive(true);
            upgradesCanvasGameObject.SetActive(false);
            PauseMenu.gameIsPaused = false;
        }
    }

    private void InitUpgradesMenu()
    {
        upgradesCanvasGameObject.GetComponent<UpgradeMenuCanvas>().Init(upgradesSystem.upgradeBranches);
    }

    //Interactive pop up disappears
    private void PopUpAppears()
    {
        interactText.SetActive(true);
    }

    //Interactive pop up disappears
    private void PopUpDisappears()
    {
        interactText.SetActive(false);
    }
}
