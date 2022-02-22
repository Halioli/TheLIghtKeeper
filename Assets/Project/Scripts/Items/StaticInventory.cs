using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventory : Inventory
{
    public new void Awake()
    {
        numberOfOccuppiedInventorySlots = 0;
        indexOfSelectedInventorySlot = 0;
        inventoryIsEmpty = true;
    }
}
