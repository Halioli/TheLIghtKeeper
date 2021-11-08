using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStack : MonoBehaviour
{
    // Attributes
    private KeyValuePair<Item, int> itemStack;

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
        itemStack = new KeyValuePair<Item, int>(item, 1);
    }

    public void InitEmptyStack()
    {
        itemStack = new KeyValuePair<Item, int>(itemNull, 0);
    }


    public bool StackIsFull() { return itemStack.Value == itemStack.Key.GetStackSize(); }

    public bool StackIsEmpty() 
    {
        return itemStack.Key == itemNull;
    }

    public bool StackHasNoItemsLeft()
    {
        return itemStack.Value == 0;
    }

    public bool StackContainsItem(Item itemToCompare)
    {
        return itemStack.Key == itemToCompare;
    }

    public bool StackHasSpaceLeft()
    {
        return itemStack.Value < itemStack.Key.GetStackSize();
    }


    public int GetItemID() { return itemStack.Key.GetID(); }

    public int GetItemStackSize() { return itemStack.Key.GetStackSize(); }

    public void AddOneItemToStack()
    {
        itemStack.Value.Equals(itemStack.Value + 1);
    }

    public void SubstractOneItemFromStack()
    {
        itemStack.Value.Equals(itemStack.Value - 1);
    }


    public Sprite GetStackItemSprite()
    {
        return itemStack.Key.GetItemSprite();
    }

    public itemStackToDisplay GetStackToDisplay()
    {
        return new itemStackToDisplay { sprite = itemStack.Key.GetItemSprite(), 
                                        quantity = itemStack.Value, 
                                        name = itemStack.Key.itemName };
    }
}
