

public class PickaxeUpgrade : Upgrade
{
    public static event UpgradeAction OnPickaxeUpgrade;

    public override void InvokeResultEvent()
    {
        if (OnPickaxeUpgrade != null)
            OnPickaxeUpgrade();
    }
}
