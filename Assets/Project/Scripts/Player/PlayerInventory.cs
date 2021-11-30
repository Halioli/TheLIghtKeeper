using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : PlayerInputs
{
    // Private Attributes
    private bool inventoryIsOpen = false;
    private Inventory inventory;

    // Public Attributes
    public Canvas inventoryCanvas;
    public InventoryMenu inventoryMenu;

    // Events
    public delegate void PlayPlayerSound();
    public static event PlayPlayerSound playerPicksUpItemEvent;



    private void Start()
    {
        inventory = GetComponentInChildren<Inventory>();
    }

    void Update()
    {
        if (PlayerPressedInventoryButton())
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
            ItemGameObject itemGameObject = GetItemGameObjectFromCollider(collider);
            PickUpItem(itemGameObject);
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

    private ItemGameObject GetItemGameObjectFromCollider(Collider2D collider)
    {
        return collider.GetComponent<ItemGameObject>();
    }

    private void PickUpItem(ItemGameObject itemToPickUp)
    {
        bool couldAddItem = inventory.AddItemToInventory(itemToPickUp.item);
        if (couldAddItem)
        {
            if (playerPicksUpItemEvent != null)
                playerPicksUpItemEvent();

            Destroy(itemToPickUp.gameObject);
        }
    }
}
