using UnityEngine;


[CreateAssetMenu(fileName = "LanternSource_Upgrade_", menuName = "Upgrade System/Upgrade/LanternSource Upgrade")]

public class LanternSourceUpgrade : Upgrade
{
    public static event UpgradeAction OnLanternSourceUpgrade;

    public override void InvokeResultEvent()
    {
        if (OnLanternSourceUpgrade != null)
            OnLanternSourceUpgrade();
    }

}
