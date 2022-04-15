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



    // should be called on application close (or on memory save)
    public int[] GetAllUpgardesLastActiveButtonIndex()
    {
        List<int> allLastActiveButtonIndex = new List<int>();

        foreach (UpgradeButtonBranch upgradeButtonBranch in upgradeButtonBranches)
        {
            allLastActiveButtonIndex.Add(upgradeButtonBranch.GetLastActiveButtonIndex());
        }   

        return allLastActiveButtonIndex.ToArray();
    }


    // must be called on Awake()
    public void FirstTimeSetAllLastCompletedButtonIndex()
    {
        for (int i = 0; i < upgradeButtonBranches.Length; ++i)
        {
            upgradeButtonBranches[i].SetLastCompletedButtonIndex(0);
        }
    }

    public void SetAllLastCompletedButtonIndex(int[] allLastCompletedButtonIndex)
    {
        for (int i = 0; i < upgradeButtonBranches.Length; ++i)
        {
            upgradeButtonBranches[i].SetLastCompletedButtonIndex(allLastCompletedButtonIndex[i]);
        }
    }


}
