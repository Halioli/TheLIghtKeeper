using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarInventory : Inventory
{
    [SerializeField] InventoryMenu inventoryMenu;


    public void CycleLeftSelectedItemIndex()
    {
        --indexOfSelectedInventorySlot;
        indexOfSelectedInventorySlot = indexOfSelectedInventorySlot < 0 ? numberOfInventorySlots - 1 : indexOfSelectedInventorySlot;
        
        SetInventroyMenuSelectedSlotIndex();
    }

    public void CycleRightSelectedItemIndex()
    {
        indexOfSelectedInventorySlot = (indexOfSelectedInventorySlot + 1) % numberOfInventorySlots;

        SetInventroyMenuSelectedSlotIndex();
    }

    public void UseSelectedConsumibleItem()
    {
        if (inventory[indexOfSelectedInventorySlot].itemInStack.itemType == ItemType.CONSUMIBLE)
        {
            GameObject consumibleItem = Instantiate(inventory[indexOfSelectedInventorySlot].itemInStack.prefab, transform.position, Quaternion.identity);
            consumibleItem.GetComponent<ItemGameObject>().DoFunctionality();

            SubstractItemFromInventorySlot(indexOfSelectedInventorySlot);
        }
    }


    private void SetInventroyMenuSelectedSlotIndex()
    {
        inventoryMenu.SetSelectedInventorySlotIndex(indexOfSelectedInventorySlot);
    }
}
