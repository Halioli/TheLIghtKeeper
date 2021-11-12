using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    // Public
    public Inventory inventory;

    // Private
    private List<ItemCell> itemCellsList;

    public void CreateInventory()
    {
        for (int i = 0; i < inventory.GetInventorySize(); i++)
        {
            Instantiate(inventory.inventory[i].itemInStack.prefab, transform);
        }
    }
}
