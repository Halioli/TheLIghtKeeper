using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCell : MonoBehaviour
{
    private InventoryMenu inventoryMenu;
    private int index;

    public Image itemImage;
    public TextMeshProUGUI itemAmount;
    public Button button;

    public void InitItemCell(InventoryMenu inventoryMenu, int index)
    {
        this.inventoryMenu = inventoryMenu;
        this.index = index;
    }

    public void SetItemImage(Sprite sprite)
    {
        itemImage.sprite = sprite;
    }

    public void SetItemAmount(int amount)
    {
        itemAmount.text = amount.ToString();
    }

    public void ClickedButton()
    {
        inventoryMenu.MoveItemToOtherInventory(index);
    }

    public void SetToEmpty()
    {
        //itemImage = empty;
        itemAmount.text = " ";
    }
}
