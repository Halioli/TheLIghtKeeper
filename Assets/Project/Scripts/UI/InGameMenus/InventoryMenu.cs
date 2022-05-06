using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    // Public
    public Inventory inventory;
    public ItemCell referenceItemCell;

    // Protected
    protected List<ItemCell> itemCellsList;
    protected int lastSelectedInventorySlot;


    private void Start()
    {
        itemCellsList = new List<ItemCell>();
        lastSelectedInventorySlot = 0;

        InitInventoryCellsList();
        SetSelectedInventorySlotIndex(lastSelectedInventorySlot);
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
        itemCellsList = new List<ItemCell>();
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
        if (itemCellsList == null) return;

        if (itemCellsList.Count < inventory.GetInventorySize())
        {
            for (int i = itemCellsList.Count; i < inventory.GetInventorySize(); ++i)
            {
                AddNewEmptyCell();
            }
        }

        SpriteRenderer sr;
        for (int i = 0; i < inventory.GetInventorySize(); ++i)
        {
            int amount = inventory.inventory[i].amountInStack;
            int ID = inventory.inventory[i].itemInStack.ID;

            bool itemChanged = itemCellsList[i].HasChanged(amount, ID);

            if (itemChanged)
            {
                sr = inventory.inventory[i].itemInStack.prefab.GetComponentInChildren<SpriteRenderer>();
                itemCellsList[i].SetItemImage(sr.sprite);
                itemCellsList[i].SetItemID(ID);
                itemCellsList[i].SetItemAmount(amount);
            }


            if (!inventory.inventory[i].StackIsEmpty())
            {
                itemCellsList[i].GetComponent<HoverButton>().SetDescription("<b>"+inventory.inventory[i].itemInStack.itemName+ ":</b>" + "\n\n" 
                    + inventory.inventory[i].itemInStack.description);
            }
        }
    }

    public void MoveItemToOtherInventory()
    {
        inventory.MoveItemToOtherInventory();
        inventory.gotChanged = true;
    }

    public virtual void SetSelectedInventorySlotIndex(int itemCellIndex)
    {
        ResetSelectedInventorySlot(itemCellIndex);
        inventory.SetSelectedInventorySlotIndex(itemCellIndex);
    }

    protected virtual void ResetSelectedInventorySlot(int newLastSelectedInventorySlot)
    {
        itemCellsList[lastSelectedInventorySlot].DoOnDiselect();
        lastSelectedInventorySlot = newLastSelectedInventorySlot;
        itemCellsList[newLastSelectedInventorySlot].DoOnSelect();
    }


    public Vector2 GetItemCellTransformPosition(int cellIndex)
    {
        return itemCellsList[cellIndex].transform.position;
    }


}
