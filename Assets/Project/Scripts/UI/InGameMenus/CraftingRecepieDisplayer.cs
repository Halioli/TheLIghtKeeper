using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CraftingRecepieDisplayer : MonoBehaviour
{
    [SerializeField] GameObject requiredMaterialPrefab;

    [SerializeField] Image resultingItemImage;
    [SerializeField] TextMeshProUGUI resultingAmountText;
    [SerializeField] TextMeshProUGUI resultingItemText;
    [SerializeField] TextMeshProUGUI resultingItemDescription;


    [SerializeField] GameObject holder;
    [SerializeField] GameObject[] requiredItems;




    public void SetRecepieResultingItem(int itemID, int itemAmount)
    {
        resultingItemImage.sprite = ItemLibrary.instance.GetItem(itemID).sprite;
        resultingAmountText.text = itemAmount + "x";
        resultingItemText.text = ItemLibrary.instance.GetItem(itemID).itemName;
        resultingItemDescription.text = ItemLibrary.instance.GetItem(itemID).description;
    }

    public void ClearRequiredMaterial(int index)
    {
        requiredItems[index].SetActive(false);
    }

    public void AddRequiredMaterial(int index, int itemID, int itemAmount)
    {
        int amountInInventory = 99;

        requiredItems[index].SetActive(true);
        requiredItems[index].GetComponent<RequiredItemDisplay>().Init(itemID, itemAmount, amountInInventory);
    }
    
}
