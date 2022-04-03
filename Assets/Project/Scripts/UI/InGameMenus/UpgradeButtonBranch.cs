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

    [SerializeField] int lastActiveButtonIndex = 0;
    int lastConnectionIndex;

    public bool upgradesAreCompleted = false;


    private void Start()
    {
        InitButtons();
        InitConnections();

        MAX.SetActive(false);
    }


    private void InitButtons()
    {
        for (int i = 0; i < upgradeButtons.Length; ++i)
        {
            bool startsEnabled = i < lastActiveButtonIndex + 1;
            upgradeButtons[i].Init(startsEnabled, upgradeBranchIndex, i, upgradeMenuCanvas);
            
            if (upgradesAreCompleted)
            {
                //upgradeButtons[i].SetDone();
                //upgradeConnection.ProgressOneStage();

                //upgradeMenuCanvas.UpgradeSelectedComplete(upgradeBranchIndex);

            }
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

    public void Complete()
    {
        MAX.SetActive(true);
    }




}
