using System.Collections.Generic;
using UnityEngine;


public class UpgradeMenuCanvas : MonoBehaviour
{
    [SerializeField] UpgradeDisplayer upgradeDisplayer;

    [SerializeField] UpgradeButtonBranch[] upgradeButtonBranches;
    [SerializeField] UpgradesSystem upgradesSystem;

    public delegate void UpgradeMenuAction();
    public static event UpgradeMenuAction OnSubmenuEnter;


    private void Start()
    {
        HideDisplay();
    }

    public void DisplayUpgrade(Upgrade upgrade, bool isCompleted)
    {
        upgradeDisplayer.gameObject.SetActive(true);

        upgradeDisplayer.SetUpgradeNameAndDescription(upgrade.upgradeName, upgrade.upgradeDescription, upgrade.longDescription);
        upgradeDisplayer.SetRequiredMaterials(upgrade.requiredItemsList, upgrade.requiredAmountsList);

        upgradeDisplayer.DisplayIsCompletedText(isCompleted);
    }

    public void HideDisplay()
    {
        upgradeDisplayer.gameObject.SetActive(false);
    }

    public bool UpgradeSelected(int upgradeBranchIndex, int upgradeIndex)
    {
        bool couldUpgrade = upgradesSystem.UpgradeBranchIsSelected(upgradeBranchIndex);

        if (couldUpgrade)
        {
            upgradeButtonBranches[upgradeBranchIndex].ProgressOneStage();

            if (upgradesSystem.UpgradeBranchIsCompleted(upgradeBranchIndex))
            {
                upgradeButtonBranches[upgradeBranchIndex].DisplayCompleteText();
            }
        }

        return couldUpgrade;
    }

    public void AlwaysProgressUpgradeSelected(int upgradeBranchIndex)
    {
        upgradesSystem.AlwaysCompleteUpgradeBranchIsSelected(upgradeBranchIndex);

        upgradeButtonBranches[upgradeBranchIndex].ProgressOneStage();

        if (upgradesSystem.UpgradeBranchIsCompleted(upgradeBranchIndex))
        {
            upgradeButtonBranches[upgradeBranchIndex].DisplayCompleteText();
        }
    }



    public void GoToSubmenu()
    {
        HideDisplay();

        if (OnSubmenuEnter != null) OnSubmenuEnter();
    }

}
