using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageStation : InteractStation
{
    [SerializeField] Inventory inventory;
    [SerializeField] GameObject storageCanvasGameObject;
    [SerializeField] InventoryMenu storageInventoryMenu;

    private bool inventoryIsOpen = false;

    public PopUp popUp;

    void Start()
    {
        storageCanvasGameObject.SetActive(inventoryIsOpen);
    }

    void Update()
    {
        if (playerInsideTriggerArea)
        {
            GetInput();            //Waits the input from InteractStation
            PopUpAppears();
        }
        else
        {
            PopUpDisappears();
            if (inventoryIsOpen)
            {
                CloseStorageInventory();
            }
        }
    }

    private void PopUpAppears()
    {
        popUp.ShowInteraction();
    }

    //Interactive pop up disappears
    private void PopUpDisappears()
    {
        popUp.HideAll();
    }

    private void OnEnable()
    {
        Inventory.OnItemMove += storageInventoryMenu.UpdateInventory;
    }

    private void OnDisable()
    {
        Inventory.OnItemMove -= storageInventoryMenu.UpdateInventory; 
    }

    override public void StationFunction()
    {
        if (inventoryIsOpen)
        {
            CloseStorageInventory();
        }
        else
        {
            OpenStorageInventory();
        }
    }

    private void OpenStorageInventory()
    {
        DoOnInteractOpen();

        inventoryIsOpen = true;
        storageCanvasGameObject.SetActive(true);
        storageInventoryMenu.UpdateInventory();

        playerInventory.SetOtherInventory(this.inventory);
        this.inventory.SetOtherInventory(playerInventory);

        PlayerInputs.instance.SetInGameMenuOpenInputs();
    }

    private void CloseStorageInventory()
    {
        DoOnInteractClose();

        inventoryIsOpen = false;
        storageCanvasGameObject.SetActive(false);

        playerInventory.SetOtherInventory(null);
        this.inventory.SetOtherInventory(null);

        PlayerInputs.instance.SetInGameMenuCloseInputs();
    }




}
