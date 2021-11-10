using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewMineralItem", menuName = "Inventory System/Items/Mineral")]

public class MineralItem : Item
{
    private void Awake()
    {
        itemType = ItemType.MINERAL;
    }

}
