using UnityEngine;


[CreateAssetMenu(fileName = "Inventory_Upgrade_", menuName = "Upgrade System/Upgrade/Inventory Upgrade")]

public class InventoryUpgrade : Upgrade
{
    public static event UpgradeAction OnInventoryUpgrade;

    public override void InvokeResultEvent()
    {
        if (OnInventoryUpgrade != null)
            OnInventoryUpgrade();
    }

}
