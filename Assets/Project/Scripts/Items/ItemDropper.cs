using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    [SerializeField] Inventory hostInventory;
    [SerializeField] float hostDropRate = 0.5f;
    [SerializeField] bool itemsWillDespawn = false;
    [SerializeField] float dropSpreadRandomness = 2f;
    [SerializeField] float notPickupableDuration = 3f;
    [SerializeField] bool spawnInDontDestroyOnLoad = true;


    protected void DropItems()
    {
        int dropAmount;
        Item item;
        for (int i = 0; i < hostInventory.inventory.Count; ++i)
        {
            if (hostInventory.inventory[i].StackIsEmpty() || hostInventory.inventory[i].itemInStack.isSpecial) continue;

            dropAmount = (int)(hostInventory.inventory[i].amountInStack * hostDropRate);
            item = hostInventory.inventory[i].itemInStack;

            hostInventory.SubstractNItemsFromInventory(item, dropAmount);

            for (int j = 0; j < dropAmount; ++j)
            {
                ItemGameObject itemGameObject = Instantiate(item.prefab, transform.position, Quaternion.identity).GetComponent<ItemGameObject>();
                itemGameObject.DropsRandom(itemsWillDespawn, dropSpreadRandomness);
                itemGameObject.MakeNotPickupableForDuration(notPickupableDuration);

                if (spawnInDontDestroyOnLoad) DontDestroyOnLoad(itemGameObject);
            }
        }


    }




}
