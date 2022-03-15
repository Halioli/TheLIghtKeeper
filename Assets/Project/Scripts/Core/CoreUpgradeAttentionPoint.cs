using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreUpgradeAttentionPoint : AttentionPoint
{
    private void OnEnable()
    {
        CoreUpgrade.OnCoreUpgrade += ActivateExclamation;
    }


    private void OnDisable()
    {
        CoreUpgrade.OnCoreUpgrade -= ActivateExclamation;
    }


    protected override void OnFadeEnd()
    {
        exclamationObject.SetActive(false);
    }

}
