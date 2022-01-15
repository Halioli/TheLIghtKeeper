using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OreVein : InteractStation
{
    // Public Attributes
    public Item materialVein;
    public Item autoMiner;
    public GameObject autoMinerGameObject;
    public GameObject interactText;
    public TextMeshProUGUI mssgText;

    // Private Attributes
    private string[] messagesToShow = { "", "No auto-miner found" };
    private bool activated = false;

    private void Start()
    {
        
    }

    void Update()
    {
        if (playerInsideTriggerArea)
        {
            GetInput();
            PopUpAppears();
        }
        else
        {
            PopUpDisappears();
        }
    }

    // Interactive pop up disappears
    private void PopUpAppears()
    {
        interactText.SetActive(true);
    }

    // Interactive pop up disappears
    private void PopUpDisappears()
    {
        interactText.SetActive(false);
        mssgText.text = messagesToShow[0];
    }

    public override void StationFunction()
    {
        if (!activated && playerInventory.InventoryContainsItem(autoMiner))
        {
            playerInventory.SubstractItemFromInventory(autoMiner);
            mssgText.text = messagesToShow[2];
        }
        else if (!activated && !playerInventory.InventoryContainsItem(autoMiner))
        {
            autoMinerGameObject.SetActive(true);

            //if (!canvasTeleportSelection.activeInHierarchy)
            //{
            //    canvasTeleportSelection.SetActive(true);
            //    PauseMenu.gameIsPaused = true;
            //}
            //else
            //{
            //    canvasTeleportSelection.SetActive(false);
            //    PauseMenu.gameIsPaused = false;
            //}
        }
    }
}
