using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBrigeMessage : TutorialMessages
{
    public delegate void BridgeTutorialDone();
    public static event BridgeTutorialDone OnBridgeTutorialDone;

    private void OnEnable()
    {
        OnBridgeTutorialDone += DisableSelf;
    }

    private void OnDisable()
    {
        OnBridgeTutorialDone -= DisableSelf;
    }

    protected override void SendMessage()
    {
        base.SendMessage();

        // Send Action
        if (OnBridgeTutorialDone != null)
            OnBridgeTutorialDone();
    }
}
