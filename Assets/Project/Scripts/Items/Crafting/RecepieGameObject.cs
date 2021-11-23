using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecepieGameObject : MonoBehaviour
{
    // Public Attributes
    public Dictionary<Item, int> requiredItems;
    public KeyValuePair<Item, int> resultingItem;

    public Recepie recepie;


    void Start()
    {
        // Initialize requiredItems
        requiredItems = new Dictionary<Item, int>();
        for (int i = 0; i < recepie.requiredItems.Count; ++i)
        {
            requiredItems[recepie.requiredItems[i]] = recepie.requiredAmounts[i];
        }

        // Initialize resultingItem
        resultingItem = new KeyValuePair<Item, int>(recepie.resultingItem, recepie.resultingAmount);
    }


}
