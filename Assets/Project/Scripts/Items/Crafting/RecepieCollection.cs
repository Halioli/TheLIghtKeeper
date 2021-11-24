using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewRecepieCollection", menuName = "Crafting System/RecepieCollection")]

public class RecepieCollection : ScriptableObject
{
    // Public Attributes
    public List<Recepie> recepies;


    public void InitRecepies()
    {
        foreach (Recepie recepie in recepies)
        {
            recepie.Init();
        }
    }
}
