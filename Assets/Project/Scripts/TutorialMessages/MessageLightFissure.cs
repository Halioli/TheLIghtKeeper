using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageLightFissure : TutorialMessages
{
    public delegate void LightFissureTutorialDone();
    public static event LightFissureTutorialDone OnLightFissureTutorial;

    private void OnEnable()
    {
        OnLightFissureTutorial += DisableSelf;
    }

    private void OnDisable()
    {
        OnLightFissureTutorial -= DisableSelf;
    }

    protected override void SendMessage()
    {
        base.SendMessage();

        // Send Action
        if (OnLightFissureTutorial != null)
            OnLightFissureTutorial();
    }
}
