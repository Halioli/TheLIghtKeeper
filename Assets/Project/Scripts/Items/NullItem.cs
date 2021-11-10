using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NullItem", menuName = "Inventory System/Items/Null")]

public class NullItem : Item
{
    private void Awake()
    {
        itemType = ItemType.NULL;
    }

}
