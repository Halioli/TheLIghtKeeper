using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    // Public
    public Inventory inventory;
    public ItemCell referenceItemCell;

    // Private
    public List<ItemCell> itemCellsList;


    private void Start()
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
            AddNewEmptyCell(i);
        }
    }


    public void AddNewEmptyCell(int listIndex)
    {
        SpriteRenderer sr = inventory.inventory[listIndex].itemInStack.prefab.GetComponent<SpriteRenderer>();
        referenceItemCell.SetItemImage(sr.sprite);

        int amount = inventory.inventory[listIndex].amountInStack;
        referenceItemCell.SetItemAmount(amount);

        itemCellsList.Add(referenceItemCell);
        itemCellsList[listIndex] = Instantiate(itemCellsList[listIndex], transform);
    }


    public void UpdateInventory()
    {
        if (itemCellsList.Count < inventory.GetInventorySize())
        {
            for (int i = itemCellsList.Count; i < inventory.GetInventorySize(); i++)
            {
                AddNewEmptyCell(i);
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
