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

    private Sprite sprite;
    public string itemName;


    private void Start()
    {
        sprite = GetComponent<Sprite>();
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

    public Sprite GetItemSprite() { return sprite; }


    public virtual void DoFunctionality() { }
}
