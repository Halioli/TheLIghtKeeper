using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Protected attributes
    public int ID; // item identifier ("type")
    public int stackSize;

    public bool isMineral;
    public bool isConsumible;

    private SpriteRenderer spriteRenderer;



    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public static bool operator== (Item itemA, Item itemB)
    {
        return itemA.ID == itemB.ID;
    }

    public static bool operator!= (Item itemA, Item itemB)
    {
        return itemA.ID != itemB.ID;
    }

    // Methods
    public int GetID() { return ID; }

    public int GetStackSize() { return stackSize; }

    public SpriteRenderer GetItemSpriteRenderer() { return spriteRenderer; }


    public virtual void DoFunctionality() { }
}
