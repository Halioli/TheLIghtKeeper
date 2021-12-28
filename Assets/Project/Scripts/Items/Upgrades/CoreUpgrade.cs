

public class CoreUpgrade : Upgrade
{
    public static event UpgradeAction OnCoreUpgrade;

    public override void InvokeResultEvent()
    {
        if (OnCoreUpgrade != null)
            OnCoreUpgrade();
    }
}
