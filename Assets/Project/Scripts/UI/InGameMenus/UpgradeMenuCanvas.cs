using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenuCanvas : MonoBehaviour
{
    [SerializeField] UpgradeButton[] upgradeButtons;
    public UpgradesSystem upgradesSystem;


    private void OnEnable()
    {
        UpgradesStation.OnInteractOpen += SetButtonsCanBeClicked;
    }

    private void OnDisable()
    {
        UpgradesStation.OnInteractOpen -= SetButtonsCanBeClicked;
    }


    public void Init()
    {
        int j = 0;

        for (int i = 0; i < upgradesSystem.upgradeBranches.Count; ++i)
        {
            upgradesSystem.upgradeBranches[i].Init(i);

            Upgrade upgrade = upgradesSystem.upgradeBranches[i].GetCurrentUpgrade();
            Sprite[] sprites = new Sprite[upgrade.requiredItems.Count];
            string[] amounts = new string[upgrade.requiredItems.Count];
            j = 0;

            foreach (KeyValuePair<Item, int> requiredItemPair in upgrade.requiredItems)
            {
                sprites[j] = requiredItemPair.Key.sprite;
                amounts[j] = requiredItemPair.Value.ToString();
                ++j;
            }
            upgradeButtons[i].InitUpdateButtonElements(upgrade.upgradeDescription, sprites, amounts);
            upgradeButtons[i].GetComponent<HoverButton>().SetDescription(upgradesSystem.upgradeBranches[i].GetCurrentUpgrade().longDescription); 

        }
    }

    public void UpgradeBranchIsSelected(int index)
    {
        if (!upgradeButtons[index].canBeClicked)
        {
            upgradesSystem.DoOnUpgardeFail();
            return;
        }

        upgradesSystem.UpgradeBranchIsSelected(index);
        upgradeButtons[index].CheckSquare();

        upgradesSystem.UpdatePlayerInventoryData();
        SetButtonsCanBeClicked();
        UpdateUpgradeButton(index);
    }

    private void UpdateUpgradeButton(int index)
    {
        if (upgradesSystem.upgradeBranches[index].IsCompleted())
        {
            upgradeButtons[index].canBeClicked = false;
            upgradeButtons[index].DisableButton();
            return;
        }

        Upgrade upgrade = upgradesSystem.upgradeBranches[index].GetCurrentUpgrade();
        Sprite[] sprites = new Sprite[upgrade.requiredItems.Count];
        string[] amounts = new string[upgrade.requiredItems.Count];
        int j = 0;

        foreach (KeyValuePair<Item, int> requiredItemPair in upgrade.requiredItems)
        {
            sprites[j] = requiredItemPair.Key.sprite;
            amounts[j] = requiredItemPair.Value.ToString();
            ++j;
        }

        upgradeButtons[index].UpdateButtonElements(upgrade.upgradeDescription, sprites, amounts);
        upgradeButtons[index].GetComponent<HoverButton>().SetDescription(upgradesSystem.upgradeBranches[index].GetCurrentUpgrade().longDescription);

    }


    void SetButtonsCanBeClicked()
    {
        for (int i=0; i < upgradeButtons.Length; ++i)
        {
            SetButtonCanBeClicked(i);
        }
    }

    void SetButtonCanBeClicked(int index)
    {
        if (upgradesSystem.upgradeBranches[index].IsCompleted())
        {
            upgradeButtons[index].GetComponent<HoverButton>().SetDescription("Upgrade branch completed.");
            return;
        }

        bool canBeClicked = upgradesSystem.PlayerHasEnoughItemsToUpgrade(upgradesSystem.upgradeBranches[index].GetCurrentUpgrade());
        upgradeButtons[index].StartClickCooldown(canBeClicked);
    }

}
