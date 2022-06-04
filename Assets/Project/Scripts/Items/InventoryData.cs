using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewInventoryData", menuName = "Inventory System/InventoryData")]


public class InventoryData : ScriptableObject
{
    public bool firstTime = true;
    //[SerializeField] ItemStack emptyItemStack;

    public List<Item> itemsData;
    public List<int> itemAmountsData;

    public void SaveInventoryItems(List<ItemStack> inventoryItems)
    {
        itemsData.Clear();
        itemAmountsData.Clear();

        if (firstTime) return;

        foreach (ItemStack itemStack in inventoryItems)
        {
            itemsData.Add(itemStack.itemInStack);
            itemAmountsData.Add(itemStack.amountInStack);
        }
    }

    public bool LoadInventoryItems(Inventory inventory)
    {
        if (firstTime)
        {
            firstTime = false;
            itemsData.Clear();
            itemAmountsData.Clear();
            return false;
        }

        for (int i = 0; i < itemsData.Count; ++i)
        {
            inventory.AddNItemsToInventory(itemsData[i], itemAmountsData[i]);
        }

        return true;
    }


}
