using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UpgradeButton : HoverButton
{
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] Image[] upgradeStatus;
    [SerializeField] GameObject[] requiredMaterials;

    private int currentUpgradeStatus = 0;
    public bool canBeClicked = true;


    public void GetsClicked()
    {
        if (canBeClicked)
        {
            transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0f), 0.25f, 3);
        }
    }


    public void InitUpdateButtonElements(string descriptionText, Sprite[] requiredMaterialImages, string[] requiredMaterialAmountTexts)
    {
        SetDescriptionText(descriptionText);

        UpdateRequiredMaterials(requiredMaterialImages.Length);
        SetRequiredMaterialImages(requiredMaterialImages);
        SetRequiredMaterialAmountTexts(requiredMaterialAmountTexts);
    }

    public void UpdateButtonElements(string descriptionText, Sprite[] requiredMaterialImages, string[] requiredMaterialAmountTexts)
    {
        if (!canBeClicked) return;

        //CheckSquare();

        SetDescriptionText(descriptionText);

        UpdateRequiredMaterials(requiredMaterialImages.Length);
        SetRequiredMaterialImages(requiredMaterialImages);
        SetRequiredMaterialAmountTexts(requiredMaterialAmountTexts);
    }

    public void CheckSquare(){
        upgradeStatus[currentUpgradeStatus].color = Color.cyan;
        ++currentUpgradeStatus;
    }

    private void SetDescriptionText(string descriptionText)
    {
        this.descriptionText.text = descriptionText;
    }

    private void SetRequiredMaterialImages(Sprite[] requiredMaterialImages)
    {
        for (int i = 0; i < requiredMaterialImages.Length; ++i)
        {
            requiredMaterials[i].GetComponentInChildren<Image>().sprite = requiredMaterialImages[i];
        }
        
    }

    private void SetRequiredMaterialAmountTexts(string[] requiredMaterialAmountTexts)
    {
        for (int i = 0; i < requiredMaterialAmountTexts.Length; ++i)
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

    IEnumerator ClickCooldown(bool canBeClicked)
    {
        this.canBeClicked = false;
        yield return new WaitForSeconds(1f);
        this.canBeClicked = canBeClicked;
    }

    public void StartClickCooldown(bool canBeClicked)
    {
        StartCoroutine(ClickCooldown(canBeClicked));
    }

    public void DisableButton()
    {
        upgradeStatus[currentUpgradeStatus].color = Color.cyan;
        ClearButton();
        GetComponent<Button>().enabled = false;
    }

    private void ClearButton()
    {
        UpdateRequiredMaterials(0);
        SetDescriptionText("Branch completed");
    }

}
