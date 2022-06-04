using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstShipUpgradeMessage : TutorialMessages
{

    private void Awake()
    {
        DisableSelf();
        if (IsTutorialFinished()) Destroy(gameObject);
    }


    public void EnableFirstShipUpgradeMessege()
    {
        EnableSelf();
    }

    protected override void SendMessage()
    {
        base.SendMessage();
        Destroy(gameObject);
    }




}
