using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMessageTeleport : TutorialMessages
{
    public delegate void TeleportTutorialDone();
    public static event TeleportTutorialDone TeleportTutorial;

    private void OnEnable()
    {
        TeleportTutorial += DisableSelf;
    }

    private void OnDisable()
    {
        TeleportTutorial -= DisableSelf;
    }

    protected override void SendMessage()
    {
        base.SendMessage();

        // Send Action
        if (TeleportTutorial != null)
            TeleportTutorial();
    }
}
