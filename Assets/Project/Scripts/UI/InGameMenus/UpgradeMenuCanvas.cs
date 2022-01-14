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
            upgradeButtons[i].UpdateButtonElements(upgrade.upgradeDescription, sprites, amounts);
        }
    }

    public void UpgradeBranchIsSelected(int index)
    {
        upgradesSystem.UpgradeBranchIsSelected(index);
        UpdateUpgradeButton(index);
        SetButtonCanBeClicked(index);
    }

    private void UpdateUpgradeButton(int index)
    {
        if (upgradesSystem.upgradeBranches[index].IsCompleted())
        {
            upgradeButtons[index].canBeClicked = false;
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
        bool canBeClicked = upgradesSystem.PlayerHasEnoughItemsToUpgrade(upgradesSystem.upgradeBranches[index].GetCurrentUpgrade());
        upgradeButtons[index].StartClickCooldown(canBeClicked);
    }

}
