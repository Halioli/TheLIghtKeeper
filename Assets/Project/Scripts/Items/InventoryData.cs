using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewInventoryData", menuName = "Inventory System/InventoryData")]


public class InventoryData : ScriptableObject
{
    [SerializeField] ItemStack emptyItemStack;
    [SerializeField] List<ItemStack> inventoryData = new List<ItemStack>();

    public void SaveInventoryItems(List<ItemStack> inventoryItems)
    {
        inventoryData.Clear();
        inventoryData = new List<ItemStack>(inventoryItems);
    }

    public void LoadInventoryItems(out List<ItemStack> inventoryItems)
    {
        inventoryItems = inventoryData;
    }



}
