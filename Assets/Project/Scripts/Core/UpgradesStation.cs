using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesStation : InteractStation
{
    public GameObject interactText;
    public GameObject backgroundText;
    
    public GameObject upgradesCanvasGameObject;
    private UpgradeMenuCanvas upgradeMenuCanvas;

    public GameObject hudGameObject;
    public UpgradesSystem upgradesSystem;

    private bool isOpen = false;

    void Start()
    {
        upgradesSystem = GetComponent<UpgradesSystem>();
        upgradesSystem.Init(playerInventory);
        upgradeMenuCanvas = upgradesCanvasGameObject.GetComponent<UpgradeMenuCanvas>();
        //InitUpgradesMenu();
        CloseUpgradesMenu();
        backgroundText.SetActive(false);
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
                CloseUpgradesMenu();
            }
        }
    }

    public override void StationFunction()
    {
        // Open menu
        if (!upgradesCanvasGameObject.activeInHierarchy)
        {
            OpenUpgradesMenu();
        }
        else
        {
            CloseUpgradesMenu();
        }
    }

    //private void InitUpgradesMenu()
    //{
    //    upgradeMenuCanvas.Init();
    //}

    //Interactive pop up disappears
    private void PopUpAppears()
    {
        interactText.SetActive(true);
        backgroundText.SetActive(true);
    }

    //Interactive pop up disappears
    private void PopUpDisappears()
    {
        interactText.SetActive(false);
        backgroundText.SetActive(false);
    }


    private void OpenUpgradesMenu()
    {
        isOpen = true;

        hudGameObject.SetActive(false);
        upgradesCanvasGameObject.SetActive(true);
        PauseMenu.PauseMineAndAttack();

        PlayerInputs.instance.SetInGameMenuOpenInputs();

        DoOnInteractOpen();
        DoOnInteractDescriptionOpen();

        upgradeMenuCanvas.HideDisplay();
    }

    private void CloseUpgradesMenu()
    {
        isOpen = false;
        hudGameObject.SetActive(true);
        upgradesCanvasGameObject.SetActive(false);
        PauseMenu.ResumeMineAndAttack();

        PlayerInputs.instance.SetInGameMenuCloseInputs();

        DoOnInteractClose();
    }
}
