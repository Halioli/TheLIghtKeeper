using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Consumible Item", menuName = "Inventory System/Items/Consumible")]

public class ConsumibleItem : Item
{
    private void Awake()
    {
        itemType = ItemType.CONSUMIBLE;
    }


    public override void DoFunctionality()
    {
        // Consumible does functionality
    }
}
