using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType { NULL, MINERAL, CONSUMIBLE }

public abstract class Item : ScriptableObject
{
    // Attributes
    public GameObject prefab;
    private Sprite sprite;


    public string itemName;
    [TextArea(5, 20)] public string description;

    public int ID; // item identifier
    public ItemType itemType;
    public int stackSize;



    //private void Start()
    //{
    //    sprite = GetComponent<Sprite>();
    //}

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
