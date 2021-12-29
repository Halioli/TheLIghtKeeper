using UnityEngine;


[CreateAssetMenu(fileName = "Pickaxe_Upgrade_", menuName = "Upgrade System/Upgrade/Pickaxe Upgrade")]

public class PickaxeUpgrade : Upgrade
{
    public static event UpgradeAction OnPickaxeUpgrade;

    public override void InvokeResultEvent()
    {
        if (OnPickaxeUpgrade != null)
            OnPickaxeUpgrade();
    }
}
