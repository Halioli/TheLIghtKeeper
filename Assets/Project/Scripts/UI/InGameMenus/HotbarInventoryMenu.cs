using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarInventoryMenu : InventoryMenu
{

    private void OnEnable()
    {
        ItemPickUp.OnItemPickUpSuccess += UpdateSelectedSlot;
    }

    private void OnDisable()
    {
        ItemPickUp.OnItemPickUpSuccess -= UpdateSelectedSlot;
    }


    private void UpdateSelectedSlot(int itemID)
    {
        ResetSelectedInventorySlot(lastSelectedInventorySlot);
    }

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
