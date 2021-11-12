using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStack : MonoBehaviour
{
    // Public Attributes
    public Item itemInStack;
    public int amountInStack;

    public struct itemStackToDisplay
    {
        public Sprite sprite;
        public int quantity;
        public string name;
    }


    public ItemStack(Item item, int amount)
    {
        itemInStack = item;
        amountInStack = amount;
    }


    // Setter Methods
    public void InitStack(Item item)
    {
        itemInStack = item;
        amountInStack = 1;
    }

    public void InitEmptyNullStack(Item item)
    {
        itemInStack = item;
        amountInStack = 0;
    }


    // Getter Methods
    public int GetItemID() { return itemInStack.GetID(); }

    public int GetItemStackSize() { return itemInStack.GetStackSize(); }
    public Sprite GetStackItemSprite()
    {
        return itemInStack.GetItemSprite();
    }

    public itemStackToDisplay GetStackToDisplay()
    {
        return new itemStackToDisplay
        {
            sprite = itemInStack.GetItemSprite(),
            quantity = amountInStack,
            name = itemInStack.itemName
        };
    }


    // Bool Methods
    public bool StackIsFull() { return amountInStack == itemInStack.GetStackSize(); }

    public bool StackHasNoItemsLeft()
    {
        return amountInStack == 0;
    }

    public bool StackContainsItem(Item itemToCompare)
    {
        return itemInStack.SameID(itemToCompare);
    }

    public bool StackHasSpaceLeft()
    {
        return amountInStack < itemInStack.GetStackSize();
    }


    // Modifier Methods
    public void AddOneItemToStack()
    {
        amountInStack++;
    }

    public void SubstractOneItemFromStack()
    {
        amountInStack--;
    }


}
