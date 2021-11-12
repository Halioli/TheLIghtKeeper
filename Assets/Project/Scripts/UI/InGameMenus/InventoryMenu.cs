using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    // Public
    public Inventory inventory;
    public ItemCell referenceItemCell;

    // Private
    private List<ItemCell> itemCellsList;




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            CreateInventory();
        }
    }

    public void CreateInventory()
    {
        for (int i = 0; i < inventory.GetInventorySize(); i++)
        {
            SpriteRenderer sr = inventory.inventory[i].itemInStack.prefab.GetComponent<SpriteRenderer>();
            referenceItemCell.SetItemImage(sr.sprite);

            int amount = inventory.inventory[i].amountInStack;
            referenceItemCell.SetItemAmount(amount);

            //Instantiate(inventory.inventory[i].itemInStack.prefab, transform);

            Instantiate(referenceItemCell, transform);
        }
    }
}
