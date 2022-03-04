using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CraftingRecepieDisplayer : MonoBehaviour
{
    [SerializeField] GameObject requiredMaterialPrefab;

    [SerializeField] Image resultingItemImage;
    [SerializeField] TextMeshProUGUI resultingItemText;
    [SerializeField] TextMeshProUGUI resultingItemDescription;


    [SerializeField] GameObject holder;
    List<GameObject> requiredItems;



    private void Awake()
    {
        requiredItems = new List<GameObject>();
    }



    public void SetRecepieResultingItem(int itemID)
    {
        resultingItemImage.sprite = ItemLibrary.instance.GetItem(itemID).sprite;
        resultingItemText.text = ItemLibrary.instance.GetItem(itemID).itemName;
        resultingItemDescription.text = ItemLibrary.instance.GetItem(itemID).description;
    }

    public void ClearCurrentRequiredMaterials()
    {
        requiredItems.Clear();
    }

    public void AddRequiredMaterial(int itemID, int itemAmount)
    {
        GameObject requiredMaterial = Instantiate(requiredMaterialPrefab, holder.transform);
        requiredMaterial.GetComponent<RequiredItemDisplay>().Init(itemID, itemAmount);
    }
    
}
