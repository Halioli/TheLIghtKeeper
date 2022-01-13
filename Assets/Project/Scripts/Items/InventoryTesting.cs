using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTesting : MonoBehaviour
{
    public Inventory inventory;

    public Canvas inventoryCanvas;
    public InventoryMenu inventoryMenu;
    private bool inventoryIsOpen = false;

    public ItemGameObject coalItemGameObject;
    public ItemGameObject ironItemGameObject;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            inventory.SubstractItemToInventory(coalItemGameObject.item);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.SubstractItemToInventory(ironItemGameObject.item);
        }

        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (inventoryIsOpen)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }

    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Item"))
        {
            ItemGameObject itemGameObject = collider.GetComponent<ItemGameObject>();
            bool couldAddItem = inventory.AddItemToInventory(itemGameObject.item);
            if (couldAddItem)
            {
                Destroy(itemGameObject.gameObject);
            }
        }
    }


    private void OpenInventory()
    {
        inventoryIsOpen = true;
        inventoryCanvas.gameObject.SetActive(true);
        inventoryMenu.UpdateInventory();
    }

    private void CloseInventory()
    {
        inventoryIsOpen = false;
        inventoryCanvas.gameObject.SetActive(false);
    }
}
