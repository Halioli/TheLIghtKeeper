

public class LanternUpgrade : Upgrade
{
    public static event UpgradeAction OnLanternUpgrade;

    public override void InvokeResultEvent()
    {
        if (OnLanternUpgrade != null)
            OnLanternUpgrade();
    }

}
