using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUpDisplayer : MonoBehaviour
{

    [SerializeField] ItemPickUpDisplay itemPickUpDisplayOriginal;

    Queue<int> displayQueue;

    int itemID, itemAmount;
    bool hasItemsToDisplay = false;
    readonly float displayCooldownDuration = 0.5f;

    Vector2 spawnPosition;
    readonly float heightToMove = 250f;
    readonly float offset = 50f;
    int offsetMultiplier = 0;

    private void Awake()
    {
        displayQueue = new Queue<int>();

        spawnPosition = GetComponent<RectTransform>().position;
    }

    private void OnEnable()
    {
        ItemPickUp.OnItemPickUpSuccess += AddItemToDisplayQueue;

        ItemPickUpDisplay.OnItemPickDisplayEnd += (() => --offsetMultiplier);
    }

    private void OnDisable()
    {
        ItemPickUp.OnItemPickUpSuccess -= AddItemToDisplayQueue;

        ItemPickUpDisplay.OnItemPickDisplayEnd -= (() => --offsetMultiplier);
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
        ItemPickUpDisplay itemPickUpDisplay = Instantiate(itemPickUpDisplayOriginal, spawnPosition, Quaternion.identity, transform);

        itemPickUpDisplay.SetDisplay(itemID, itemAmount);
        itemPickUpDisplay.DoDisplayAnimation(heightToMove - (offset * offsetMultiplier++));
    }


    IEnumerator DisplayCooldown()
    {
        while (hasItemsToDisplay)
        {
            yield return new WaitForSecondsRealtime(displayCooldownDuration);

            SetSameItemAmountInDisplayQueue();
            DisplayPickedUpItem();

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


}
