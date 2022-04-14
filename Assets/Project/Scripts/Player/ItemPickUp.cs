using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private PlayerInventory playerInventory;
    [SerializeField] Collider2D itemPickUpCheckCollider;
    [SerializeField] CircleCollider2D itemPickUpCollider;
    ItemGameObject itemGameObject;


    public delegate void PlayPlayerSound();
    public static event PlayPlayerSound playerPicksUpItemEvent;
    public delegate void ItemPickUpSuccessAction(int itemID);
    public static event ItemPickUpSuccessAction OnItemPickUpSuccess;

    bool isOnItemPickUpFailInvoked = false;
    readonly float onItemPickUpFailDuration = 3f;
    public delegate void ItemPickUpFailAction(float isOnItemPickUpFailDuration);
    public static event ItemPickUpFailAction OnItemPickUpFail;



    private void Start()
    {
        playerInventory = GetComponentInParent<PlayerInventory>();
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Item"))
        {
            itemGameObject = collider.GetComponent<ItemGameObject>();

            if (itemGameObject.isPickedUpAlready)
            {
                if (collider.IsTouching(itemPickUpCollider))
                {
                    DestroyPickedUpItem();
                }

            }
            else
            {
                if (collider.IsTouching(itemPickUpCheckCollider))
                {
                    if (!itemGameObject.permanentNotPickedUp && playerInventory.hotbarInventory.ItemCanBeAdded(itemGameObject.item))
                    {
                        ItemPickUpSuccess();
                    }
                    else
                    {
                        ItemPickUpFail();
                    }
                }

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


    private void ItemPickUpFail()
    {
        itemGameObject.SetSelfStatic();
        itemGameObject.canBePickedUp = false;

        if (Vector2.Distance(itemGameObject.transform.position, transform.position) > 1.5f) return;

        if (!isOnItemPickUpFailInvoked) StartCoroutine(DoOnItemPickUpFail());
    }

    IEnumerator DoOnItemPickUpFail()
    {
        isOnItemPickUpFailInvoked = true;

        if (OnItemPickUpFail != null) OnItemPickUpFail(onItemPickUpFailDuration);
        
        yield return new WaitForSeconds(onItemPickUpFailDuration);

        isOnItemPickUpFailInvoked = false;
    }



    private void ItemPickUpSuccess()
    {
        //itemGameObject.canBePickedUp = playerInventory.hotbarInventory.AddItemToInventory(itemGameObject.item);

        //if (OnItemPickUpSuccess != null) OnItemPickUpSuccess(itemGameObject.item.ID);

        itemGameObject.isPickedUpAlready = true;
    }


    private void DestroyPickedUpItem()
    {
        itemGameObject.canBePickedUp = playerInventory.hotbarInventory.AddItemToInventory(itemGameObject.item);

        if (OnItemPickUpSuccess != null) OnItemPickUpSuccess(itemGameObject.item.ID);


        Destroy(itemGameObject.gameObject);
        if (playerPicksUpItemEvent != null) playerPicksUpItemEvent();
    }



}
