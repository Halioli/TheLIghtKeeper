using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewConsumibleItem", menuName = "Inventory System/Items/Consumible")]

public class ConsumibleItem : Item
{
    private void Awake()
    {
        itemType = ItemType.CONSUMIBLE;
    }

}
