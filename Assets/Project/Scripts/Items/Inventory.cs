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
    private bool inventoryIsEmpty;

    private const int MAX_NUMBER_OF_SLOTS = 9;


    // Methods
    protected void Start()
    {
        numberOfInventorySlots = 4;
        numberOfOccuppiedInventorySlots = 0;
        indexOfSelectedInventorySlot = 0;
        inventoryIsEmpty = false;

        InitInventory();
    }


    public void InitInventory()
    {
        ItemStack newItemStackToAdd = new ItemStack();
        newItemStackToAdd.InitEmptyStack();
        for (int i = 0; i < numberOfInventorySlots; i++)
        {
            inventory.Add(newItemStackToAdd);
        }
    }

    public void UpgradeInventory()
    {
        if (numberOfInventorySlots < MAX_NUMBER_OF_SLOTS)
        {
            numberOfInventorySlots++;
            ItemStack newItemStackToAdd = new ItemStack();
            newItemStackToAdd.InitEmptyStack();
            inventory.Add(newItemStackToAdd);
        }
    }



    public bool InventoryIsEmpty()
    {
        return inventoryIsEmpty;
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
            if (inventory[i].StackContainsItem(itemToCompare) && inventory[i].StackHasSpaceLeft())
            {
                return i;
            }
            i++;
        }
        return -1;
    }

    public int NextInventorySlotWithAvailableItemToSubstract(Item itemToCompare)
    {
        int i = 0;
        while (i < numberOfInventorySlots)
        {
            if (inventory[i].StackContainsItem(itemToCompare))
            {
                return i;
            }
            i++;
        }
        return -1;
    }


    public int NextEmptyInventorySlot()
    {
        int i = 0;
        while (i < numberOfInventorySlots)
        {
            if (inventory[i].StackIsEmpty())
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

        // Instantiate and initialize the item to add
        ItemStack newItemStackToAdd = new ItemStack();
        newItemStackToAdd.InitStack(itemToAdd);

        // Check if the inventory is empty, to add item directly
        if (InventoryIsEmpty())
        {
            inventory[0] = newItemStackToAdd;
            numberOfOccuppiedInventorySlots++;
            inventoryIsEmpty = false;
            couldAddItem = true;
        }
        else
        {
            int index = NextInventorySlotWithAvailableSpaceToAddItem(itemToAdd);
            if (index != -1)
            {
                // Add to slot in use
                inventory[index].AddOneItemToStack();
                couldAddItem = true;
            }
            else
            {
                index = NextEmptyInventorySlot();
                if (index != -1)
                {
                    inventory[index] = newItemStackToAdd;
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
        int index = NextInventorySlotWithAvailableItemToSubstract(itemToSubstract);

        if (index != -1)
        {
            inventory[index].SubstractOneItemFromStack();

            // Set slot to empty if the last unit of the item was substracted
            if (inventory[index].StackHasNoItemsLeft())
            {
                inventory[index].InitEmptyStack();
                numberOfOccuppiedInventorySlots--;

                if (numberOfOccuppiedInventorySlots == 0)
                {
                    inventoryIsEmpty = true;
                }
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
