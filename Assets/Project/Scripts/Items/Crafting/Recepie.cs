using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewRecepie", menuName = "Crafting System/Recepie")]

public class Recepie : ScriptableObject
{
    // Public Attributes
    public string recepieName;

    public List<Item> requiredItemsList;
    public List<int> requiredAmountsList;

    public Item resultingItemUnit;
    public int resultingAmountUnit;

    public Dictionary<Item, int> requiredItems;
    public KeyValuePair<Item, int> resultingItem;


    public void Init()
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
