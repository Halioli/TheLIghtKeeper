using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    // Public
    public Inventory inventory;
    public ItemCell referenceItemCell;

    // Private
    private List<ItemCell> itemCellsList = new List<ItemCell>();
    private int lastSelectedInventorySlot = 0;


    private void Awake()
    {
        InitInventoryCellsList();
        SetSelectedInventorySlotIndex(0);
    }


    private void Update()
    {
        if (inventory.gotChanged)
        {
            inventory.gotChanged = false;
            UpdateInventory();
        }
    }


    public void InitInventoryCellsList()
    {
        for (int i = 0; i < inventory.GetInventorySize(); ++i)
        {
            AddNewEmptyCell();
        }
    }


    public void AddNewEmptyCell()
    {
        ItemCell newItemCell = Instantiate(referenceItemCell, transform).GetComponent<ItemCell>();

        itemCellsList.Add(newItemCell);

        newItemCell.InitItemCell(this, itemCellsList.Count - 1);

        //SpriteRenderer sr = inventory.inventory[itemCellsList.Count - 1].itemInStack.prefab.GetComponent<SpriteRenderer>();
        ////referenceItemCell.SetItemImage(sr.sprite);
        //newItemCell.SetItemImage(sr.sprite);

        //int amount = inventory.inventory[itemCellsList.Count - 1].amountInStack;
        ////referenceItemCell.SetItemAmount(amount);
        //newItemCell.SetItemAmount(amount);
    }


    public void UpdateInventory()
    {
        if (itemCellsList.Count < inventory.GetInventorySize())
        {
            for (int i = itemCellsList.Count; i < inventory.GetInventorySize(); ++i)
            {
                AddNewEmptyCell();
            }
        }

        for (int i = 0; i < inventory.GetInventorySize(); ++i)
        {
            SpriteRenderer sr = inventory.inventory[i].itemInStack.prefab.GetComponent<SpriteRenderer>();
            itemCellsList[i].SetItemImage(sr.sprite);

            int amount = inventory.inventory[i].amountInStack;
            itemCellsList[i].SetItemAmount(amount);

            if (!inventory.inventory[i].StackIsEmpty())
            {
                itemCellsList[i].GetComponent<HoverButton>().SetDescription(inventory.inventory[i].itemInStack.description);
            }
        }
    }


    public void SetSelectedInventorySlotIndex(int itemCellIndex)
    {
        ResetSelectedInventorySlot(itemCellIndex);
        inventory.SetSelectedInventorySlotIndex(itemCellIndex);
    }


    public void MoveItemToOtherInventory()
    {
        inventory.MoveItemToOtherInventory();
        inventory.gotChanged = true;
    }


    private void ResetSelectedInventorySlot(int newLastSelectedInventorySlot)
    {
        itemCellsList[lastSelectedInventorySlot].DoOnDiselect();
        lastSelectedInventorySlot = newLastSelectedInventorySlot;
        itemCellsList[newLastSelectedInventorySlot].DoOnSelect();
    }
}
