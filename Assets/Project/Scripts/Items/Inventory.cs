using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Inventory : MonoBehaviour
{
    // Public Attributes
    public Item itemNull;
    public ItemStack emptyStack;

    public List<ItemStack> inventory = new List<ItemStack>();

    public int indexOfSelectedInventorySlot;

    public bool gotChanged = false;

    // Private Attributes
    [SerializeField] protected int numberOfInventorySlots;
    protected int numberOfOccuppiedInventorySlots;
    protected bool inventoryIsEmpty;

    [SerializeField] private int maxNumberOfSlots;


    private Inventory otherInventory = null;


    // Action
    public delegate void InventoryAction();
    public static event InventoryAction OnItemMove;


    // Initializer Methods
    public void Awake()
    {
        numberOfOccuppiedInventorySlots = 0;
        indexOfSelectedInventorySlot = 0;
        inventoryIsEmpty = true;


        InitInventory();
    }


    public void InitInventory()
    {
        for (int i = 0; i < numberOfInventorySlots; i++)
        {
            inventory.Add(Instantiate(emptyStack, transform));
        }
        gotChanged = true;
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
        int i = numberOfInventorySlots-1;
        while (i >= 0)
        {
            if (inventory[i].StackContainsItem(itemToCompare))
            {
                return i;
            }
            --i;
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

    public bool ItemCanBeAdded(Item itemToCompare)
    {
        return (NextEmptyInventorySlot() != -1) ||
               (NextInventorySlotWithAvailableSpaceToAddItem(itemToCompare) != -1);
    }

    // Modifier Methods
    public void UpgradeInventory()
    {
        if (numberOfInventorySlots < maxNumberOfSlots)
        {
            numberOfInventorySlots++;
            inventory.Add(Instantiate(emptyStack, transform));
            gotChanged = true;
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

    public bool InventoryContainsItemAndAmount(Item itemToCompare, int requiredAmount)
    {
        bool hasEnough = false;
        int i = 0;
        int amountInInventory = 0;
        while (!hasEnough && i < numberOfInventorySlots)
        {
            if (inventory[i].StackContainsItem(itemToCompare))
            {
                amountInInventory += inventory[i].GetAmountInStack();
            }
            hasEnough = amountInInventory >= requiredAmount;
            i++;
        }
        return hasEnough;
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

        if (couldAddItem)
        {
            gotChanged = true;
        }

        return couldAddItem;
    }


    public bool SubstractItemFromInventory(Item itemToSubstract)
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

        if (couldRemoveItem)
        {
            gotChanged = true;
        }
    
        return couldRemoveItem;
    }

    public bool SubstractNItemsFromInventory(Item itemToSubstract, int numberOfItemsToSubstract)
    {
        for (int i = 0; i < numberOfItemsToSubstract; ++i)
        {
            if (!SubstractItemFromInventory(itemToSubstract))
            {
                return false;
            }
        }
        return true;
    }

    public void SubstractItemFromInventorySlot(int inventorySlot)
    {
        inventory[inventorySlot].SubstractOneItemFromStack();

        // Set slot to empty if the last unit of the item was substracted
        if (inventory[inventorySlot].StackHasNoItemsLeft())
        {
            inventory[inventorySlot].InitEmptyNullStack(itemNull);
            numberOfOccuppiedInventorySlots--;

            if (numberOfOccuppiedInventorySlots == 0)
            {
                inventoryIsEmpty = true;
            }
        }

        gotChanged = true;
    }

    // Other Methods
    public List<ItemStack.itemStackToDisplay> Get3ItemsToDisplayInHUD()
    {
        int i = indexOfSelectedInventorySlot;
        int n = numberOfInventorySlots;

        List<ItemStack.itemStackToDisplay> itemsToDisplay = new List<ItemStack.itemStackToDisplay>();

        if (((i - 1) % n) < 0)
        {
            itemsToDisplay.Add(inventory[3].GetStackToDisplay());
        }
        else
        {
            itemsToDisplay.Add(inventory[(i - 1) % n].GetStackToDisplay());
        }
        itemsToDisplay.Add(inventory[i].GetStackToDisplay());
        itemsToDisplay.Add(inventory[(i + 1) % n].GetStackToDisplay());

        return itemsToDisplay;
    }

    public void CycleLeftSelectedItemIndex()
    {
        --indexOfSelectedInventorySlot;
        indexOfSelectedInventorySlot = indexOfSelectedInventorySlot < 0 ? indexOfSelectedInventorySlot = numberOfInventorySlots-1 : indexOfSelectedInventorySlot;

    }

    public void CycleRightSelectedItemIndex()
    {
        indexOfSelectedInventorySlot = (indexOfSelectedInventorySlot + 1) % numberOfInventorySlots;
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


    public void MoveItemToOtherInventory(int itemCellIndex)
    {
        if (otherInventory == null) return;
        if (inventory[itemCellIndex].itemInStack == itemNull) return;


        bool canSwap = otherInventory.ItemCanBeAdded(inventory[itemCellIndex].itemInStack);

        if (canSwap)
        {
            otherInventory.AddItemToInventory(inventory[itemCellIndex].itemInStack);
            SubstractItemFromInventorySlot(itemCellIndex);

            otherInventory.gotChanged = true;

            if (OnItemMove != null) OnItemMove();
        }
    }

    public void SetOtherInventory(Inventory otherInventory)
    {
        this.otherInventory = otherInventory;
    }

}
