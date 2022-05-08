using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSavingInventory : Inventory
{
    [SerializeField] InventoryData inventoryData;




    protected void OnEnable()
    {
        BrokenFurnace.OnTutorialFinish += SaveInventory;
        PauseMenu.OnGameExit += SaveInventory;
    }

    protected void OnDisable()
    {
        BrokenFurnace.OnTutorialFinish -= SaveInventory;
        PauseMenu.OnGameExit -= SaveInventory;
    }


    public void SaveInventory()
    {
        inventoryData.SaveInventoryItems(inventory);
    }

    public void LoadInventory()
    {
        inventoryData.LoadInventoryItems(this);
    }


    public override void InitInventory()
    {
        base.InitInventory(); // Resets the whole inventory (all stacks are empty)
        LoadInventory();

        gotChanged = true;
        inventoryIsEmpty = false;
    }

}
