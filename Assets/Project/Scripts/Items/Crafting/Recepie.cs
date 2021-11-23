using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewRecepie", menuName = "Crafting System/Recepie")]

public class Recepie : ScriptableObject
{
    // Public Attributes
    public string recepieName;

    public GameObject recepieGameObjectPrefab;

    public List<Item> requiredItems;
    public List<int> requiredAmounts;

    public Item resultingItem;
    public int resultingAmount;
}
