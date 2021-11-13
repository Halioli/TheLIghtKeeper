using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTesting : MonoBehaviour
{
    public Inventory inventory;

    public ItemGameObject coalItemGameObject;
    public ItemGameObject ironItemGameObject;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.UpgradeInventory();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            inventory.CycleLeftSelectedItemIndex();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            inventory.CycleRightSelectedItemIndex();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            inventory.SubstractItemToInventory(coalItemGameObject.item);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.SubstractItemToInventory(ironItemGameObject.item);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            inventory.UpgradeInventory();
        }
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Item"))
        {
            ItemGameObject itemGameObject = collider.GetComponent<ItemGameObject>();
            inventory.AddItemToInventory(itemGameObject.item);
        }
    }

}
