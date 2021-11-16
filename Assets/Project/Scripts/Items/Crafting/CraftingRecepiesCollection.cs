using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecepiesCollection : MonoBehaviour
{
    public List<CraftingRecepie> listoOfRecepies;



    public int GetNumberOfRecepies()
    {
        return listoOfRecepies.Count;
    }

    public CraftingRecepie GetRecepieWithIndex(int index)
    {
        return listoOfRecepies[index];
    }


    public void AddRecepie(CraftingRecepie craftingRecepieToAdd)
    {
        listoOfRecepies.Add(craftingRecepieToAdd);
    }

    public void AddAllRecepiesFromOtherCollection(CraftingRecepiesCollection otherRecepieCollection)
    {
        for (int i = 0; i < GetNumberOfRecepies(); i++)
        {
            listoOfRecepies.Add(otherRecepieCollection.GetRecepieWithIndex(i));
        }
    }


}
