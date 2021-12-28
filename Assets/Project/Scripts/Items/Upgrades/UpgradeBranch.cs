using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewUpgradeBranch", menuName = "Upgrade System/UpgradeBranch")]

public class UpgradeBranch : ScriptableObject
{
    // Public Attributes
    public string upgradeBranchName;
    List<Upgrade> upgrades;


    // Private Attributes
    private int numberOfUpgrades;
    private int currentUpgrade;



    public void Init()
    {
        numberOfUpgrades = upgrades.Capacity;
        currentUpgrade = 0;
    }


}
