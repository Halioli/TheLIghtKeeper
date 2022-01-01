using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // Private Attributes
    private bool inventoryIsOpen = false;
    private Collider2D itemCollectionCollider;

    // Public Attributes
    public Inventory inventory { get; private set; }
    public Canvas inventoryCanvas;
    public InventoryMenu inventoryMenu;

    // Events
    public delegate void PlayPlayerSound();
    public static event PlayPlayerSound playerPicksUpItemEvent;



    private void Start()
    {
        inventory = GetComponentInChildren<Inventory>();
        itemCollectionCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if (PlayerInputs.instance.PlayerPressedInventoryButton())
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

            if (collider.IsTouching(itemCollectionCollider))
            {
                if (itemGameObject.canBePickedUp)
                {
                    PickUpItem(itemGameObject);
                    Debug.Log("adding");
                }
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

    private ItemGameObject GetItemGameObjectFromCollider(Collider2D collider)
    {
        return collider.GetComponent<ItemGameObject>();
    }

    private bool PickUpItem(ItemGameObject itemToPickUp)
    {
        //bool couldAddItem = inventory.AddItemToInventory(itemToPickUp.item);
        //if (couldAddItem)
        //{
        //    if (playerPicksUpItemEvent != null)
        //        playerPicksUpItemEvent();

        //    Destroy(itemToPickUp.gameObject);
        //}

        //return couldAddItem;

        if (playerPicksUpItemEvent != null)
            playerPicksUpItemEvent();

        Destroy(itemToPickUp.gameObject);
        return false;
    }

}
