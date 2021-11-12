using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Inventory : MonoBehaviour
{
    // Public Attributes
    public Item itemNull;
    public ItemStack emptyStack;

    public List<ItemStack> inventory = new List<ItemStack>();


    // Private Attributes
    private int numberOfInventorySlots;
    private int numberOfOccuppiedInventorySlots;
    private int indexOfSelectedInventorySlot;
    private bool inventoryIsEmpty;

    private const int MAX_NUMBER_OF_SLOTS = 9;



    // Initializer Methods
    public void Start()
    {
        numberOfInventorySlots = 4;
        numberOfOccuppiedInventorySlots = 0;
        indexOfSelectedInventorySlot = 0;
        inventoryIsEmpty = true;


        InitInventory();
    }

    public void InitInventory()
    {
        //ItemStack newItemStackToAdd = new ItemStack();
        //newItemStackToAdd.InitEmptyNullStack(itemNull);
        //for (int i = 0; i < numberOfInventorySlots; i++)
        //{
        //    inventory.Add(newItemStackToAdd);
        //}


        for (int i = 0; i < numberOfInventorySlots; i++)
        {
            inventory.Add(Instantiate(emptyStack, transform));
        }
        
    }


    // Getter Methods
    public int GetInventorySize() { return numberOfInventorySlots; }

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
            //ItemStack newItemStackToAdd = new ItemStack();
            //newItemStackToAdd.InitEmptyNullStack(itemNull);
            //inventory.Add(newItemStackToAdd);
            inventory.Add(Instantiate(emptyStack, transform));
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

        // Check if the inventory is empty, to add item directly
        if (InventoryIsEmpty())
        {
            inventory[0].InitStack(itemToAdd);

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
                    inventory[index].InitStack(itemToAdd);
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
