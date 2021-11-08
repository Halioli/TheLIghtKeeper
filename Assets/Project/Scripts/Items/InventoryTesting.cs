using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTesting : MonoBehaviour
{
    public Inventory inventory;

    public Item coalItem;

    public ItemsInHUD itemsInHUD;


    private void Start()
    {
        itemsInHUD.GetItemsToDisplay();
    }


    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.UpgradeInventory();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            inventory.CycleLeftSelectedItemIndex();
            itemsInHUD.UpdateHUDItemsToDisplay();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            inventory.CycleRightSelectedItemIndex();
            itemsInHUD.UpdateHUDItemsToDisplay();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            inventory.AddItemToInventory(coalItem);
            itemsInHUD.UpdateHUDItemsToDisplay();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            inventory.SubstractItemToInventory(coalItem);
            itemsInHUD.UpdateHUDItemsToDisplay();
        }
    }

}
