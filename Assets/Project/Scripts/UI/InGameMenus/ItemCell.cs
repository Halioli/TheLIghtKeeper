using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class ItemCell : HoverButton
{
    protected InventoryMenu inventoryMenu;
    protected int index;

    public Image itemImage;
    public TextMeshProUGUI itemAmount;
    public Button button;

    private int amount = -1;

    public void InitItemCell(InventoryMenu inventoryMenu, int index)
    {
        this.inventoryMenu = inventoryMenu;
        this.index = index;
    }

    public void SetItemImage(Sprite sprite)
    {
        itemImage.sprite = sprite;
    }


    public int GetItemAmount()
    {
        return amount;
    }


    public void SetItemAmount(int amount)
    {
        this.amount = amount;

        if (amount == 0)
        {
            SetToEmpty();
            return;
        }
        itemAmount.text = amount.ToString();


        ItemSlotChangedAnimation();
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
    public virtual void DoOnSelect(bool isConsumible) { }
    public virtual void DoOnDiselect() { }


    private void ItemSlotChangedAnimation()
    {
        itemImage.transform.DOComplete();
        itemImage.transform.DOPunchScale(new Vector3(-0.2f, 0.4f), 0.1f, 2);
    }



}
