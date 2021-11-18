using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingMenu : MonoBehaviour
{
    // Private Attribute
    private Inventory playerInventory;

    // Public Attribute
    public Item lightRodItem;
    public InventoryMenu inventoryMenu;

    private void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
    }

    public void GiveItemToPlayer()
    {
        playerInventory.AddItemToInventory(lightRodItem);
        inventoryMenu.UpdateInventory();
    }
}
