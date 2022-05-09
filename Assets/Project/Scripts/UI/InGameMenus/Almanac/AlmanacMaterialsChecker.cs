using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlmanacMaterialsChecker : MonoBehaviour
{
    Dictionary<int, AlmanacScriptableObject> materialsChecklist;

    public Item[] items;
    public AlmanacScriptableObject[] almanacScriptableObjectMaterials;

    private void Awake()
    {
        materialsChecklist = new Dictionary<int, AlmanacScriptableObject>();
        for (int i = 0; i < items.Length; ++i)
        {
            materialsChecklist.Add(items[i].ID, almanacScriptableObjectMaterials[i]);
        }
    }


    private void OnEnable()
    {
        ItemPickUp.OnItemPickUpSuccess += CheckItemID;
    }

    private void OnDisable()
    {
        ItemPickUp.OnItemPickUpSuccess -= CheckItemID;
    }


    private void CheckItemID(int itemID)
    {

        if (!ItemLibrary.instance.GetItem(itemID).existsInAlmanac) return;

        bool isNew = !materialsChecklist[itemID].hasBeenFound;

        if (isNew)
        {
            materialsChecklist[itemID].hasBeenFound = true;
        }

    }

}
