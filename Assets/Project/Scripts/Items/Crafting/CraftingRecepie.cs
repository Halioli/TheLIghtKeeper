using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CraftingRecepie : MonoBehaviour
{
    // Public Attributes
    [Header("Requierd items and their respective amount: ")]
    public List<Item> requiredItems;
    public List<int> requiredAmounts;

    [Header("Resulting item and its amount: ")]
    public Item resultingItem;
    public int resultingItemAmount;

    public ItemStack emptyItemStack;


    // Private Attributes
    private List<ItemStack> requiredItemStacks;
    private ItemStack resultingItemStack;


    private void Awake()
    {
        // Initialize the required items List of ItemStacks
        requiredItemStacks = new List<ItemStack>();
        //requiredItemStacks.Capacity = requiredItems.Count;
        for (int i = 0; i < requiredItems.Count; i++)
        {
            //requiredItemStacks[i] = new ItemStack(requiredItems[i], requiredAmounts[i]);
            //ItemStack(requiredItems[i], requiredAmounts[i])
            emptyItemStack.itemInStack = requiredItems[i];
            emptyItemStack.amountInStack = requiredAmounts[i];

            requiredItemStacks.Add(Instantiate(emptyItemStack, transform));
        }

        // Initialize the resulting item ItemStack
        resultingItemStack = new ItemStack(resultingItem, resultingItemAmount);
    }

    public List<ItemStack> GetRequiredItemStacks()
    {
        return requiredItemStacks;
    }

    public ItemStack GetResultingItemStack()
    {
        return resultingItemStack;
    }
}
