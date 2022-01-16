using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private PlayerInventory playerInventory;
    private CircleCollider2D itemPickUpCheckCollider;

    private void Start()
    {
        playerInventory = GetComponentInParent<PlayerInventory>();
        itemPickUpCheckCollider = GetComponent<CircleCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.IsTouching(itemPickUpCheckCollider)) return;

        if (collider.gameObject.CompareTag("Item"))
        {
            ItemGameObject itemGameObject = collider.GetComponent<ItemGameObject>();
            
            if (!itemGameObject.permanentNotPickedUp && playerInventory.inventory.ItemCanBeAdded(itemGameObject.item))
            {
                itemGameObject.canBePickedUp = playerInventory.inventory.AddItemToInventory(itemGameObject.item);
            }
            else
            {
                itemGameObject.SetSelfStatic();
                itemGameObject.canBePickedUp = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Item"))
        {
            collider.GetComponent<ItemGameObject>().SetSelfDynamic();
        }
    }

}
