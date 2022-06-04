using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlmanacEnvironmentChecker : MonoBehaviour
{
    Dictionary<int, AlmanacScriptableObject> materialsChecklist;

    public int[] IDs;
    public AlmanacScriptableObject[] almanacScriptableObjectMaterials;


    public delegate void AlmanacEnvirnomentCheckAction();
    public static event AlmanacEnvirnomentCheckAction OnNewItemFound;



    private void Awake()
    {
        materialsChecklist = new Dictionary<int, AlmanacScriptableObject>();
        for (int i = 0; i < IDs.Length; ++i)
        {
            materialsChecklist.Add(IDs[i], almanacScriptableObjectMaterials[i]);
        }

    }


    private void OnEnable()
    {
        AlmanacEnvironmentTrigger.OnEnvironmentTrigger += CheckItemID;
    }

    private void OnDisable()
    {
        AlmanacEnvironmentTrigger.OnEnvironmentTrigger -= CheckItemID;
    }


    private void CheckItemID(int ID)
    {
        bool isNew = !materialsChecklist[ID].hasBeenFound;

        if (isNew)
        {
            materialsChecklist[ID].hasBeenFound = true;

            if (OnNewItemFound != null) OnNewItemFound();
        }

    }


    public AlmanacScriptableObject[] GetItems()
    {
        return almanacScriptableObjectMaterials;
    }

    public void InitItems(bool[] itemsHasBeenFound)
    {
        for (int i = 0; i < almanacScriptableObjectMaterials.Length; ++i)
        {
            almanacScriptableObjectMaterials[i].hasBeenFound = itemsHasBeenFound[i];
        }
    }


}
