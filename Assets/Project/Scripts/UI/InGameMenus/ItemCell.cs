using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCell : HoverButton
{
    protected InventoryMenu inventoryMenu;
    protected int index;

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

    public void SetToEmpty()
    {
        itemAmount.text = " ";
    }

    public override void DoDescriptionTextAction()
    {
        if (inventoryMenu.inventory.inventory[index].StackIsEmpty())
        {
            base.DoOnHover();
            return;
        }
        base.DoDescriptionTextAction();
    }


    public void ClickedButton()
    {
        inventoryMenu.SetSelectedInventorySlotIndex(index);
        inventoryMenu.MoveItemToOtherInventory();
    }

    public virtual void DoOnSelect() { }
    public virtual void DoOnDiselect() { }
}
