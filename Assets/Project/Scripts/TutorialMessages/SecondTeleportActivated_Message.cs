using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondTeleportActivated_Message : TutorialMessages
{
    public delegate void SecondActivationTutorialDone();
    public static event SecondActivationTutorialDone OnSecondActivationTutorialDone;

    private void OnEnable()
    {
        OnSecondActivationTutorialDone += DisableSelf;
        TeleportSystem.OnSecondTeleportActive += SendMessage;
    }

    private void OnDisable()
    {
        OnSecondActivationTutorialDone -= DisableSelf;
        TeleportSystem.OnSecondTeleportActive -= SendMessage;
    }

    protected override void SendMessage()
    {
        base.SendMessage();

        // Send Action
        if (OnSecondActivationTutorialDone != null)
            OnSecondActivationTutorialDone();
    }
}
