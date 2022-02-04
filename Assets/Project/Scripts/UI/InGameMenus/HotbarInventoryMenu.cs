using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarInventoryMenu : InventoryMenu
{


    protected override void ResetSelectedInventorySlot(int newLastSelectedInventorySlot)
    {
        itemCellsList[lastSelectedInventorySlot].DoOnDiselect();
        lastSelectedInventorySlot = newLastSelectedInventorySlot;

        bool isConsumible = false;
        if (inventory.inventory[newLastSelectedInventorySlot].StackIsEmpty())
        {
            itemCellsList[newLastSelectedInventorySlot].DoOnSelect(isConsumible);
        }
        else
        {
            isConsumible = inventory.inventory[newLastSelectedInventorySlot].itemInStack.itemType == ItemType.CONSUMIBLE;
            itemCellsList[newLastSelectedInventorySlot].DoOnSelect(isConsumible);
        }
    }

}
