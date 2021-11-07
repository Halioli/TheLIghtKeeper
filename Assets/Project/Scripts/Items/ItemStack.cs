using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStack : MonoBehaviour
{
    // Attributes
    private KeyValuePair<Item, int> itemStack;

    public Item itemNull;


    // Methods
    public void InitStack(Item item) {
        itemStack = new KeyValuePair<Item, int>(item, 1);
    }

    public void InitEmptyStack()
    {
        itemStack = new KeyValuePair<Item, int>(itemNull, 0);
    }


    public bool StackIsFull() { return itemStack.Value == itemStack.Key.GetStackSize(); }

    public bool StackIsEmpty() {
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

    public void AddOneItem()
    {
        itemStack.Value.Equals(itemStack.Value + 1);
    }

    public void SubstractOneItem()
    {
        itemStack.Value.Equals(itemStack.Value - 1);
    }


    public SpriteRenderer GetStackItemSpriteRenderer()
    {
        return itemStack.Key.GetItemSpriteRenderer();
    }

    public KeyValuePair<SpriteRenderer, int> GetStackItemSpriteRendererAndUnitsPair()
    {
        return new KeyValuePair<SpriteRenderer, int>(itemStack.Key.GetItemSpriteRenderer(), itemStack.Value);
    }
}
