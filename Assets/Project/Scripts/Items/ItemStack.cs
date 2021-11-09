using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStack : MonoBehaviour
{
    // Attributes
    private Item itemInStack;
    private int amountInStack;

    public Item itemNull;


    public struct itemStackToDisplay
    {
        public Sprite sprite;
        public int quantity;
        public string name;
    }

    private void Awake()
    {
        InitEmptyStack();
    }


    // Methods
    public void InitStack(Item item) 
    {
        itemInStack = item;
        amountInStack = 1;
    }

    public void InitEmptyStack()
    {
        itemInStack = itemNull;
        amountInStack = 0;
    }


    public bool StackIsFull() { return amountInStack == itemInStack.GetStackSize(); }

    public bool StackIsEmpty() 
    {
        return itemInStack.SameID(itemNull);
    }

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


    public int GetItemID() { return itemInStack.GetID(); }

    public int GetItemStackSize() { return itemInStack.GetStackSize(); }

    public void AddOneItemToStack()
    {
        amountInStack++;
    }

    public void SubstractOneItemFromStack()
    {
        amountInStack--;
    }


    public Sprite GetStackItemSprite()
    {
        return itemInStack.GetItemSprite();
    }

    public itemStackToDisplay GetStackToDisplay()
    {
        return new itemStackToDisplay { sprite = itemInStack.GetItemSprite(), 
                                        quantity = amountInStack, 
                                        name = itemInStack.itemName };
    }
}
