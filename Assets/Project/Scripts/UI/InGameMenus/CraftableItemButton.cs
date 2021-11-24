using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftableItemButton : MonoBehaviour
{
    // Private Attributes

    // Public Attributes
    public int buttonNumber = 0;

    public void CraftSelectedItem()
    {

        Debug.Log(buttonNumber);
        if (CheckIfInventoryHasMaterials())
        {
            RemoveMaterialsFromInventory();

            AddItemsToInventory();

            Debug.Log("ITEM CRAFTED");
        }
        else
        {
            Debug.Log("NOT ENOUGH MATERIALS");
        }
    }

    private bool CheckIfInventoryHasMaterials()
    {
        return false;
    }

    private void RemoveMaterialsFromInventory()
    {

    }

    private void AddItemsToInventory()
    {

    }

    private void Ping()
    {
        Debug.Log("Ping");
    }
}
