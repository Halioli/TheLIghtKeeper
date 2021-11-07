using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Attributes
    private List<ItemStack> inventory;
    private int numberOfInventorySlots;
    private int numberOfOccuppiedInventorySlots;
    private int indexOfSelectedInventorySlot;


    // Methods
    protected void Start()
    {
        numberOfInventorySlots = 4;
        numberOfOccuppiedInventorySlots = 0;
        indexOfSelectedInventorySlot = 0;

        inventory.Capacity = numberOfInventorySlots;
    }


    public bool InventoryIsEmpty()
    {
        return inventory.Capacity == 0;
    }

    public bool InventoryContainsItem(Item itemToCompare)
    {
        bool wasFound = false;
        int i = 0;
        while (!wasFound && i < numberOfInventorySlots)
        {
            wasFound = inventory[i].StackContainsItem(itemToCompare);
            i++;
        }
        return wasFound;
    }

    public int NextInventorySlotWithAvailableSpaceToAddItem(Item itemToCompare)
    {
        int i = 0;
        while (i < numberOfInventorySlots)
        {
            if ((inventory[i].StackContainsItem(itemToCompare) && inventory[i].StackHasSpaceLeft()) ||
                inventory[i].StackIsEmpty())
            {
                return i;
            }
            i++;
        }
        return -1;
    }



    public bool AddItemToInventory(Item itemToAdd)
    {
        bool couldAddItem = false;

        // Check if there are empty slots
        if (numberOfOccuppiedInventorySlots < numberOfInventorySlots)
        {
            // Instantiate and initialize the item to add
            ItemStack newItemStackToAdd = new ItemStack();
            newItemStackToAdd.InitStack(itemToAdd);
            
            // Check if the inventory is empty, to add item directly
            if (InventoryIsEmpty())
            {
                inventory[0] = newItemStackToAdd;
                numberOfOccuppiedInventorySlots++;
                couldAddItem = true;
            }

            // Find if there is an available slot to add the item
            else
            {
                int index = NextInventorySlotWithAvailableSpaceToAddItem(itemToAdd);

                // If an adequate slot is found..
                if (index != -1)
                {
                    // Add to empty slot
                    if (inventory[index].StackIsEmpty())
                    {
                        inventory[index] = newItemStackToAdd; 
                    }
                    // Add to slot in use
                    else
                    {
                        inventory[index].AddOneItem();
                    }
                    numberOfOccuppiedInventorySlots++;
                    couldAddItem = true;
                }
            }
        }

        return couldAddItem;
    }


    public bool SubstractItemToInventory(Item itemToSubstract)
    {
        bool couldRemoveItem = false;

        // Check if the inventory contains an item to substract 
        if (InventoryContainsItem(itemToSubstract))
        {
            int index = NextInventorySlotWithAvailableSpaceToAddItem(itemToSubstract);
            inventory[index].SubstractOneItem();

            // Set slot to empty if the last unit of the item was substracted
            if (inventory[index].StackHasNoItemsLeft())
            {
                inventory[index].InitEmptyStack();
            }
            couldRemoveItem = true;
        }

        return couldRemoveItem;
    }


    public List<KeyValuePair<SpriteRenderer, int>> Get3ItemsToDisplayInHUD()
    {
        int i = indexOfSelectedInventorySlot;
        int n = numberOfInventorySlots;

        List<KeyValuePair<SpriteRenderer, int>> itemsToDisplay = new List<KeyValuePair<SpriteRenderer, int>>();

        itemsToDisplay.Add(inventory[(i - 1) % n].GetStackItemSpriteRendererAndUnitsPair());
        itemsToDisplay.Add(inventory[i].GetStackItemSpriteRendererAndUnitsPair());
        itemsToDisplay.Add(inventory[(i + 1) % n].GetStackItemSpriteRendererAndUnitsPair());

        return itemsToDisplay;
    }


    // This function might belong to another script
    public void UpdateItemsToDisplayInHUD()
    {
        List<KeyValuePair<SpriteRenderer, int>> itemsToDisplay = Get3ItemsToDisplayInHUD();

        // Show 1st Item
        // Show 2nd Item (Central)
        // Show 3rd Item

    }



}
