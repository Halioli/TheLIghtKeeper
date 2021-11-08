using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsInHUD : MonoBehaviour
{
    // Public
    public Inventory inventory;
    public HUDItem itemLeft;
    public HUDItem itemCenter;
    public HUDItem itemRight;

    // Private
    private List<ItemStack.itemStackToDisplay> itemsToDisplay;

    public void GetItemsToDisplay()
    {
        itemsToDisplay = inventory.Get3ItemsToDisplayInHUD();
    }

    public void UpdateHUDItemsToDisplay()
    {
        itemLeft.SetImage(itemsToDisplay[0].sprite);
        itemLeft.SetTextQuantity((itemsToDisplay[0].quantity).ToString());

        itemCenter.SetImage(itemsToDisplay[1].sprite);
        itemCenter.SetTextQuantity((itemsToDisplay[1].quantity).ToString());
        itemCenter.SetTextName(itemsToDisplay[1].name);

        itemRight.SetImage(itemsToDisplay[2].sprite);
        itemRight.SetTextQuantity((itemsToDisplay[2].quantity).ToString());
    }
}
