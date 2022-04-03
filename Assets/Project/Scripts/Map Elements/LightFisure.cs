using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFisure : InteractStation
{
    // Public Attributes
    public Item lightGenerator;
    public GameObject lightGeneratorGameObject;
    public PopUp popUp;

    // Private Attributes
    private string[] messagesToShow = { "", "No <b>Light Generator</b> found" };
    private bool activated = false;

    private void Start()
    {
        lightGeneratorGameObject.SetActive(activated);
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
        popUp.ShowInteraction();
    }

    // Interactive pop up disappears
    private void PopUpDisappears()
    {
        popUp.HideAll();
        popUp.ChangeMessageText(messagesToShow[0]);
    }

    public override void StationFunction()
    {
        if (activated) return;

        popUp.ShowMessage();
        if (playerInventory.InventoryContainsItem(lightGenerator))
        {
            activated = true;

            playerInventory.SubstractItemFromInventory(lightGenerator);
            popUp.ChangeMessageText(messagesToShow[0]);
            lightGeneratorGameObject.SetActive(true);

            //lightGeneratorGameObject.GetComponent<AutoMiner>().GetsPlacedDown(materialVein);
            popUp.gameObject.SetActive(false);
        }
        else
        {
            popUp.ChangeMessageText(messagesToShow[1]);

            InvokeOnNotEnoughMaterials();
        }
    }
}
