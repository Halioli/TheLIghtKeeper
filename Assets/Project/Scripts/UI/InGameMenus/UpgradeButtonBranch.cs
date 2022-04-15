using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    public delegate void UpgradeButtonAction(string upgradeName, Sprite upgradeSprite);



    private void Start()
    {
        MAX.SetActive(false);

        InitButtons();
        InitCompletedButtons();
        
        InitConnections();
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



    // should be called on application close (or on memory save)
    public int GetLastActiveButtonIndex()
    {
        return lastActiveButtonIndex;
    }

    // must be called on Awake()
    public void SetLastCompletedButtonIndex(int lastCompletedButtonIndex)
    {
        this.lastCompletedButtonIndex = lastCompletedButtonIndex;
    }



    public void GetUpgradeNameAndIcon(int upgradeIndex, out string upgradeName, out Image upgradeIcon)
    {
        upgradeButtons[upgradeIndex].GetNameAndIcon(out upgradeName, out upgradeIcon);
    }

}
