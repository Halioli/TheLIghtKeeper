using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesStation : InteractStation
{
    public GameObject interactText;
    public GameObject upgradesCanvasGameObject;
    public GameObject hudGameObject;
    public UpgradesSystem upgradesSystem;

    private bool isOpen = false;
    private UpgradeMenuCanvas upgradeMenuCanvas;
    //private InventoryMenu inventoryMenu;

    void Start()
    {
        upgradesSystem = GetComponent<UpgradesSystem>();
        upgradesSystem.Init(playerInventory);
        upgradeMenuCanvas = upgradesCanvasGameObject.GetComponent<UpgradeMenuCanvas>();
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
                CloseStorageInventory();
            }
        }
    }

    public override void StationFunction()
    {
        // Open menu
        if (!upgradesCanvasGameObject.activeInHierarchy)
        {
            OpenStorageInventory();
        }
        else
        {
            CloseStorageInventory();
        }
    }

    private void InitUpgradesMenu()
    {
        upgradeMenuCanvas.Init();
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


    private void OpenStorageInventory()
    {
        isOpen = true;

        hudGameObject.SetActive(false);
        upgradesCanvasGameObject.SetActive(true);
        //inventoryMenu.UpdateInventory();
        PauseMenu.gameIsPaused = true;

        DoOnInteractOpen();
    }

    private void CloseStorageInventory()
    {
        isOpen = false;
        hudGameObject.SetActive(true);
        upgradesCanvasGameObject.SetActive(false);
        PauseMenu.gameIsPaused = false;

        DoOnInteractClose();
    }
}
