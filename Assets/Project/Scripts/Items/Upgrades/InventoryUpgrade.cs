

public class InventoryUpgrade : Upgrade
{
    public static event UpgradeAction OnInventoryUpgrade;

    public override void InvokeResultEvent()
    {
        if (OnInventoryUpgrade != null)
            OnInventoryUpgrade();
    }

}
