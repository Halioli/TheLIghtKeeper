using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstShipUpgradeMessage : TutorialMessages
{
    public delegate void FirstShipUpgradeDone();
    public static event FirstShipUpgradeDone OnFirstShipUpgradeDone;

    private void Awake()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    private void OnEnable()
    {
        FirstShipUpgradeMessage.OnFirstShipUpgradeDone += DisableSelf;
        CoreUpgrade.OnCoreUpgrade += () => GetComponent<Collider2D>().enabled = true;
    }

    private void OnDisable()
    {
        FirstShipUpgradeMessage.OnFirstShipUpgradeDone -= DisableSelf;
        CoreUpgrade.OnCoreUpgrade -= () => GetComponent<Collider2D>().enabled = true;
    }

    protected override void SendMessage()
    {
        base.SendMessage();
    }
}
