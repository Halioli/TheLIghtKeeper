using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrade System/Upgrade")]

public class Upgrade : ScriptableObject
{
    // Public Attributes
    public string upgradeName;
    public List<Item> requiredItemsList;
    public List<int> requiredAmountsList;
    public Dictionary<Item, int> requiredItems;

    public delegate void UpgradeAction();


    public void Init()
    {
        // Initialize requiredItems
        requiredItems = new Dictionary<Item, int>();
        for (int i = 0; i < requiredItemsList.Count; ++i)
        {
            requiredItems[requiredItemsList[i]] = requiredAmountsList[i];
        }
    }

    public virtual void InvokeResultEvent() { }

}