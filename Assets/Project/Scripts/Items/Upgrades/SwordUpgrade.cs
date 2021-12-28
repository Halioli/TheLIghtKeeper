

public class SwordUpgrade : Upgrade
{
    public static event UpgradeAction OnSwordUpgrade;

    public override void InvokeResultEvent()
    {
        if (OnSwordUpgrade != null)
            OnSwordUpgrade();
    }
}
