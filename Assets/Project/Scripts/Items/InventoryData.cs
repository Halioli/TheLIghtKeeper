using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewInventoryData", menuName = "Inventory System/InventoryData")]


public class InventoryData : ScriptableObject
{
    public bool firstTime = true;
    [SerializeField] ItemStack emptyItemStack;
    public List<KeyValuePair<Item, int>> inventoryData = new List<KeyValuePair<Item, int>>();

    public void SaveInventoryItems(List<ItemStack> inventoryItems)
    {
        inventoryData.Clear();

        foreach (ItemStack itemStack in inventoryItems)
        {
            inventoryData.Add(new KeyValuePair<Item, int>(itemStack.itemInStack, itemStack.amountInStack));
        }
    }

    public bool LoadInventoryItems(Inventory inventory, Transform transform)
    {
        foreach (KeyValuePair<Item, int> stackData in inventoryData)
        {
            inventory.AddNItemsToInventory(stackData.Key, stackData.Value);
        }

        return true;
    }


}
