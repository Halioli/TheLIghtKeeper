using UnityEngine;


[CreateAssetMenu(fileName = "Core_Upgrade_", menuName = "Upgrade System/Upgrade/Core Upgrade")]

public class CoreUpgrade : Upgrade
{
    public delegate void UpgradeAction();
    public static event UpgradeAction OnCoreUpgrade;

    public override void InvokeResultEvent()
    {
        if (OnCoreUpgrade != null)
            OnCoreUpgrade();
    }
}
