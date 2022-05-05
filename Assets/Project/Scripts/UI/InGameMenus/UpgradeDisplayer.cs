using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeDisplayer : MonoBehaviour
{
    [SerializeField] GameObject requiredMaterialPrefab;

    [SerializeField] TextMeshProUGUI upgradeNameText;
    [SerializeField] TextMeshProUGUI upgradeDescriptionText;
    [SerializeField] TextMeshProUGUI upgradeLongDescriptionText;
    [SerializeField] GameObject completedText;

    [SerializeField] GameObject[] requiredItems;


    public void SetUpgradeNameAndDescription(string upgradeName, string upgradeDescription, string upgradeLongDescription)
    {
        upgradeNameText.text = upgradeName;
        upgradeDescriptionText.text = upgradeDescription;
        upgradeLongDescriptionText.text = upgradeLongDescription;
    }

    public void SetRequiredMaterials(List<Item> items, List<int> amounts)
    {
        for (int i = 0; i < items.Count; ++i)
        {
            AddRequiredMaterial(i, items[i].GetID(), amounts[i]);
        }

        for (int i = items.Count; i < requiredItems.Length; ++i)
        {
            ClearRequiredMaterial(i);
        }
    }

    public void DisplayIsCompletedText(bool isCompleted)
    {
        completedText.SetActive(isCompleted);
    }



    private void ClearRequiredMaterial(int index)
    {
        requiredItems[index].SetActive(false);
    }

    private void AddRequiredMaterial(int index, int itemID, int itemAmount)
    {
        requiredItems[index].SetActive(true);
        requiredItems[index].GetComponent<RequiredItemDisplay>().Init(itemID, itemAmount);
    }




}
