using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUpDisplayer : MonoBehaviour
{

    [SerializeField] ItemPickUpDisplay itemPickUpDisplayOriginal;

    Queue<int> displayQueue;
    Dictionary<int, ItemPickUpDisplay> activeDisplays;

    int itemID, itemAmount;
    bool hasItemsToDisplay = false;
    readonly float displayCooldownDuration = 0.5f;

    Vector2 spawnPosition;
    float startSpawnY;
    readonly float offset = 100f;
    int offsetMultiplier = 0;

    private void Awake()
    {
        displayQueue = new Queue<int>();
        activeDisplays = new Dictionary<int, ItemPickUpDisplay>();

        spawnPosition = GetComponent<RectTransform>().position;
        startSpawnY = spawnPosition.y;
    }

    private void OnEnable()
    {
        ItemPickUp.OnItemPickUpSuccess += AddItemToDisplayQueue;

        ItemPickUpDisplay.OnItemPickDisplayEnd += DeleteActiveDisplay;
    }

    private void OnDisable()
    {
        ItemPickUp.OnItemPickUpSuccess -= AddItemToDisplayQueue;

        ItemPickUpDisplay.OnItemPickDisplayEnd -= DeleteActiveDisplay;
    }



    private void AddItemToDisplayQueue(int itemID)
    {
        displayQueue.Enqueue(itemID);

        StartDisplayingItems();
    }

    private void SetSameItemAmountInDisplayQueue()
    {
        itemID = displayQueue.Dequeue();
        itemAmount = 1;

        while (displayQueue.Count > 0 && itemID == displayQueue.Peek())
        {
            displayQueue.Dequeue();
            ++itemAmount;
        }
    }

    private void DisplayPickedUpItem()
    {
        spawnPosition.y = startSpawnY + (offset * offsetMultiplier++);
        //ItemPickUpDisplay itemPickUpDisplay = Instantiate(itemPickUpDisplayOriginal, spawnPosition, Quaternion.identity, transform);
        ItemPickUpDisplay itemPickUpDisplay = Instantiate(itemPickUpDisplayOriginal, transform);

        activeDisplays.Add(itemID, itemPickUpDisplay);

        itemPickUpDisplay.SetDisplay(itemID, itemAmount);
    }


    IEnumerator DisplayCooldown()
    {
        while (hasItemsToDisplay)
        {
            yield return new WaitForSecondsRealtime(displayCooldownDuration);

            SetSameItemAmountInDisplayQueue();

            if (activeDisplays.ContainsKey(itemID))
            {
                activeDisplays[itemID].UpdateDisplayText(itemAmount);
            }
            else
            {
                DisplayPickedUpItem();
            }

            hasItemsToDisplay = displayQueue.Count > 0;
        }
    }

    private void StartDisplayingItems()
    {
        if (!hasItemsToDisplay)
        {
            hasItemsToDisplay = true;
            StartCoroutine(DisplayCooldown());
        }
    }


    private void DeleteActiveDisplay(int itemID)
    {
        //bool itemIDFound = false;
        
        //foreach (KeyValuePair<int, ItemPickUpDisplay> displayPair in activeDisplays)
        //{
        //    if (displayPair.Key == itemID) itemIDFound = true;
            
        //    else if (itemIDFound)
        //    {
        //        activeDisplays[displayPair.Key].ResetPosition(offset);
        //    }
        //}

        Destroy(activeDisplays[itemID].gameObject);
        activeDisplays.Remove(itemID);

        --offsetMultiplier;
    }


}
