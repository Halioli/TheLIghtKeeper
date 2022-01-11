using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageStation : InteractStation
{
    [SerializeField] Inventory inventory;
    [SerializeField] GameObject storageCanvasGameObject;
    [SerializeField] InventoryMenu storageInventoryMenu;

    private bool inventoryIsOpen = false;


    void Start()
    {
        storageCanvasGameObject.SetActive(inventoryIsOpen);
    }

    void Update()
    {
        if (playerInsideTriggerArea)
        {
            GetInput();            //Waits the input from InteractStation 

        }
        else
        {
            if (inventoryIsOpen)
            {
                CloseStorageInventory();
            }
        }
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
        inventoryIsOpen = true;
        storageCanvasGameObject.SetActive(true);
        storageInventoryMenu.UpdateInventory();

        playerInventory.SetOtherInventory(this.inventory);
        this.inventory.SetOtherInventory(playerInventory);
    }

    private void CloseStorageInventory()
    {
        inventoryIsOpen = false;
        storageCanvasGameObject.SetActive(false);

        playerInventory.SetOtherInventory(null);
        this.inventory.SetOtherInventory(null);
    }




}
