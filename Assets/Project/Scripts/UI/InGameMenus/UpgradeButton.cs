using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] Image[] upgradeStatus;
    [SerializeField] GameObject[] requiredMaterials;
    private int currentUpgradeStatus = 0;


    public void GetsClicked()
    {
        transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0f), 0.25f, 3);
    }

    public void UpdateButtonElements(string descriptionText, Sprite[] requiredMaterialImages, string[] requiredMaterialAmountTexts)
    {
        upgradeStatus[currentUpgradeStatus++].color = Color.cyan;
        SetDescriptionText(descriptionText);

        UpdateRequiredMaterials(requiredMaterialImages.Length);
        SetRequiredMaterialImages(requiredMaterialImages);
        SetRequiredMaterialAmountTexts(requiredMaterialAmountTexts);
    }

    private void SetDescriptionText(string descriptionText)
    {
        this.descriptionText.text = descriptionText;
    }

    private void SetRequiredMaterialImages(Sprite[] requiredMaterialImages)
    {
        for (int i = 0; i < requiredMaterials.Length; ++i)
        {
            requiredMaterials[i].GetComponentInChildren<Image>().sprite = requiredMaterialImages[i];
        }
        
    }

    private void SetRequiredMaterialAmountTexts(string[] requiredMaterialAmountTexts)
    {
        for (int i = 0; i < requiredMaterials.Length; ++i)
        {
            requiredMaterials[i].GetComponentInChildren<TMP_Text>().text = requiredMaterialAmountTexts[i];
        }
    }

    private void UpdateRequiredMaterials(int amount)
    {
        for (int i = 0; i < requiredMaterials.Length; ++i)
        {
            if (i < amount)
            {
                requiredMaterials[i].GetComponent<CanvasGroup>().alpha = 1;
            }
            else
            {
                requiredMaterials[i].GetComponent<CanvasGroup>().alpha = 0;
            }
        }

    }

}
