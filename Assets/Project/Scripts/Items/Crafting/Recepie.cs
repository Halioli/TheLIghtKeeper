using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recepie : MonoBehaviour
{
    // Private Attributes
    private Dictionary<Item, int> requiredItems;
    private KeyValuePair<Item, int> resultingItem;

    // Public Attributes
    public List<Item> requiredItemsList;
    public List<int> requiredAmountsList;

    public Item resultingItemUnit;
    public int resultingAmountUnit;


    private void Start()
    {
        // Initialize requiredItems
        requiredItems = new Dictionary<Item, int>();
        for (int i = 0; i < requiredItemsList.Count; ++i)
        {
            requiredItems[requiredItemsList[i]] = requiredAmountsList[i];
        }

        // Initialize resultingItem
        resultingItem = new KeyValuePair<Item, int>(resultingItemUnit, resultingAmountUnit);
    }
}
