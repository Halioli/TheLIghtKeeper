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
    [SerializeField] GameObject lockedText;
    [SerializeField] GameObject completedText;

    [SerializeField] TextMeshProUGUI requiredText;
    [SerializeField] GameObject[] requiredItems;


    public void SetUpgradeNameAndDescription(string upgradeName, string upgradeDescription, string upgradeLongDescription)
    {
        upgradeNameText.text = upgradeName;
        upgradeDescriptionText.text = upgradeDescription;
        upgradeLongDescriptionText.text = upgradeLongDescription;
    }

    public void SetRequiredMaterials(List<Item> items, List<int> amounts, int[] amountsInInventory)
    {
        requiredText.gameObject.SetActive(true);

        for (int i = 0; i < items.Count; ++i)
        {
            AddRequiredMaterial(i, items[i].GetID(), amounts[i], amountsInInventory[i]);
        }

        for (int i = items.Count; i < requiredItems.Length; ++i)
        {
            ClearRequiredMaterial(i);
        }
    }

    public void HideRequiredMaterials()
    {
        requiredText.gameObject.SetActive(false);

        for (int i = 0; i < requiredItems.Length; ++i)
        {
            ClearRequiredMaterial(i);
        }
    }

    public void DisplayIsCompletedText(bool isCompleted)
    {
        completedText.SetActive(isCompleted);
    }

    public void DisplayLockedText()
    {
        lockedText.SetActive(true);
    }

    public void HideLockedText()
    {
        lockedText.SetActive(false);
    }


    private void ClearRequiredMaterial(int index)
    {
        requiredItems[index].SetActive(false);
    }

    private void AddRequiredMaterial(int index, int itemID, int itemAmount, int amountInInventory)
    {
        requiredItems[index].SetActive(true);
        requiredItems[index].GetComponent<RequiredItemDisplay>().Init(itemID, itemAmount, amountInInventory);
    }




}
