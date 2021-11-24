using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingMenu : MonoBehaviour
{
    // Private Attribute
    private Inventory playerInventory;

    // Public Attribute
    public Item lightRodItem;
    public Item coalItem;
    public Item ironItem;
    public InventoryMenu inventoryMenu;

    private void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
    }

    public bool CheckPlayerInventoryForRequiredItems()
    {
        if (playerInventory.InventoryContainsItem(coalItem) && playerInventory.InventoryContainsItem(ironItem))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SubstractItemsFromInventory()
    {
        playerInventory.SubstractItemToInventory(coalItem);
        playerInventory.SubstractItemToInventory(ironItem);
    }

    public void GiveItemToPlayer()
    {
        if (CheckPlayerInventoryForRequiredItems())
        {
            SubstractItemsFromInventory();

            playerInventory.AddItemToInventory(lightRodItem);
            inventoryMenu.UpdateInventory();
        }
    }
}
