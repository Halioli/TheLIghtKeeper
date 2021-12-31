using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    //[SerializeField] PlayerInventory playerInventory;

    //private void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if (collider.gameObject.CompareTag("Item") && collider.IsTouching(playerInventory.itemCollectionCollider))
    //    {
    //        ItemGameObject itemGameObject = GetItemGameObjectFromCollider(collider);
    //        if (itemGameObject.canBePickedUp)
    //        {
    //            if (!PickUpItem(itemGameObject))
    //            {
    //                itemGameObject.SetSelfStatic();
    //            }
    //        }
    //        else
    //        {
    //            itemGameObject.SetSelfStatic();
    //        }
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collider)
    //{
    //    if (collider.gameObject.CompareTag("Item"))
    //    {
    //        ItemGameObject itemGameObject = GetItemGameObjectFromCollider(collider);
    //        itemGameObject.SetSelfDynamic();
    //    }
    //}

}
