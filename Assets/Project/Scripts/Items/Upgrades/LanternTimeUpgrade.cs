using UnityEngine;


[CreateAssetMenu(fileName = "LanternTime_Upgrade_", menuName = "Upgrade System/Upgrade/LanternTime Upgrade")]

public class LanternTimeUpgrade : Upgrade
{
    public static event UpgradeAction OnLanternTimeUpgrade;

    public override void InvokeResultEvent()
    {
        if (OnLanternTimeUpgrade != null)
            OnLanternTimeUpgrade();
    }

}
