using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]

public class Inventory : ScriptableObject
{
    // Public Attributes
    public List<ItemStack> inventory = new List<ItemStack>();
    public Item itemNull;

    // Private Attributes
    private int numberOfInventorySlots;
    private int numberOfOccuppiedInventorySlots;
    private int indexOfSelectedInventorySlot;
    private bool inventoryIsEmpty;

    private const int MAX_NUMBER_OF_SLOTS = 9;



    // Constructors and Initializer Methods
    public Inventory()
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
        newItemStackToAdd.InitEmptyNullStack(itemNull);
        for (int i = 0; i < numberOfInventorySlots; i++)
        {
            inventory.Add(newItemStackToAdd);
        }
    }


    // Getter Methods
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
            if (inventory[i].StackHasNoItemsLeft())
            {
                return i;
            }
            i++;
        }
        return -1;
    }



    // Modifier Methods
    public void UpgradeInventory()
    {
        if (numberOfInventorySlots < MAX_NUMBER_OF_SLOTS)
        {
            numberOfInventorySlots++;
            ItemStack newItemStackToAdd = new ItemStack();
            newItemStackToAdd.InitEmptyNullStack(itemNull);
            inventory.Add(newItemStackToAdd);
        }
    }


    // Bool Methods
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
                inventory[index].InitEmptyNullStack(itemNull);
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



    // Other Methods
    public List<ItemStack.itemStackToDisplay> Get3ItemsToDisplayInHUD()
    {
        int i = indexOfSelectedInventorySlot;
        int n = numberOfInventorySlots;

        List<ItemStack.itemStackToDisplay> itemsToDisplay = new List<ItemStack.itemStackToDisplay>();

        itemsToDisplay.Add(inventory[(i - 1) % n].GetStackToDisplay());
        itemsToDisplay.Add(inventory[i].GetStackToDisplay());
        itemsToDisplay.Add(inventory[(i + 1) % n].GetStackToDisplay());

        return itemsToDisplay;
    }


    public void CycleLeftSelectedItemIndex()
    {
        indexOfSelectedInventorySlot = (indexOfSelectedInventorySlot - 1) % numberOfInventorySlots;
    }

    public void CycleRightSelectedItemIndex()
    {
        indexOfSelectedInventorySlot = (indexOfSelectedInventorySlot + 1) % numberOfInventorySlots;
    }


}
