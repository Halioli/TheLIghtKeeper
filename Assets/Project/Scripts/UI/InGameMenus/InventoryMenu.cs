using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    // Public
    public Inventory inventory;
    public ItemCell referenceItemCell;

    // Private
    public List<ItemCell> itemCellsList = new List<ItemCell>();


    private void Awake()
    {
        InitInventoryCellsList();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UpdateInventory();
        }
    }



    private void InitInventoryCellsList()
    {
        for (int i = 0; i < inventory.GetInventorySize(); i++)
        {
            AddNewEmptyCell();
        }
    }


    public void AddNewEmptyCell()
    {
        itemCellsList.Add(Instantiate(referenceItemCell, transform).GetComponent<ItemCell>());
       
        SpriteRenderer sr = inventory.inventory[itemCellsList.Count - 1].itemInStack.prefab.GetComponent<SpriteRenderer>();
        referenceItemCell.SetItemImage(sr.sprite);

        int amount = inventory.inventory[itemCellsList.Count - 1].amountInStack;
        referenceItemCell.SetItemAmount(amount);

    }

    public void UpdateInventory()
    {
        if (itemCellsList.Count < inventory.GetInventorySize())
        {
            for (int i = itemCellsList.Count; i < inventory.GetInventorySize(); i++)
            {
                AddNewEmptyCell();
            }
        }

        for (int i = 0; i < inventory.GetInventorySize(); i++)
        {
            SpriteRenderer sr = inventory.inventory[i].itemInStack.prefab.GetComponent<SpriteRenderer>();
            itemCellsList[i].SetItemImage(sr.sprite);

            int amount = inventory.inventory[i].amountInStack;
            itemCellsList[i].SetItemAmount(amount);
        }
    }
}
