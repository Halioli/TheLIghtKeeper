using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Null Item", menuName = "Inventory System/Items/Default")]

public class NullItem : Item
{
    private void Awake()
    {
        itemType = ItemType.NULL;
    }

}
