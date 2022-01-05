using UnityEngine;


[CreateAssetMenu(fileName = "Sword_Upgrade_", menuName = "Upgrade System/Upgrade/Sword Upgrade")]

public class SwordUpgrade : Upgrade
{
    public static event UpgradeAction OnSwordUpgrade;

    public override void InvokeResultEvent()
    {
        if (OnSwordUpgrade != null)
            OnSwordUpgrade();
    }
}
