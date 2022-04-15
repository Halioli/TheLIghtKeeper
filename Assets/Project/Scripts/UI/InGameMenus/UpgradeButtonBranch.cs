using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtonBranch : MonoBehaviour
{
    [SerializeField] int upgradeBranchIndex;
    [SerializeField] UpgradeButton[] upgradeButtons;
    [SerializeField] UpgradeConnection upgradeConnection;
    [SerializeField] UpgradeMenuCanvas upgradeMenuCanvas;
    [SerializeField] GameObject MAX;

    int lastActiveButtonIndex = 0;
    [SerializeField] int lastCompletedButtonIndex = 0;
    int lastConnectionIndex;


    private void Start()
    {
        InitButtons();
        InitCompletedButtons();
        
        InitConnections();


        MAX.SetActive(false);
    }


    private void InitButtons()
    {
        // First, enable the next active upgrade and disable the following
        for (int i = 0; i < upgradeButtons.Length; ++i)
        {
            bool startsEnabled = i < lastActiveButtonIndex + 1;
            upgradeButtons[i].Init(startsEnabled, upgradeBranchIndex, i, upgradeMenuCanvas);
        }
    }

    private void InitCompletedButtons()
    {
        // Second, progress the upgrades that were completed
        for (int i = 0; i < lastCompletedButtonIndex; ++i)
        {
            upgradeButtons[i].AlwaysProgressUpgradeSelected();
        }
    }



    private void InitConnections()
    {
        lastConnectionIndex = lastActiveButtonIndex - 1;

        upgradeConnection.Init(lastConnectionIndex);
    }


    public void ProgressOneStage()
    {
        upgradeButtons[lastActiveButtonIndex++].SetDone();
        
        if (lastActiveButtonIndex < upgradeButtons.Length)
        {
            upgradeButtons[lastActiveButtonIndex].EnableButton();

            upgradeConnection.ProgressOneStage();
            ++lastConnectionIndex;
        }

    }

    public void DisplayCompleteText()
    {
        MAX.SetActive(true);
    }




}
