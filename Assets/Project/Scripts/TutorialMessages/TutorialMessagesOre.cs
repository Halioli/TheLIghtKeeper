using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMessagesOre : TutorialMessages
{
    public delegate void OreTutorialDone();
    public static event OreTutorialDone OreTutorial;

    private void OnEnable()
    {
        OreTutorial += DisableSelf;
    }

    private void OnDisable()
    {
        OreTutorial -= DisableSelf;
    }

    protected override void SendMessage()
    {
        base.SendMessage();

        // Send Action
        if (OreTutorial != null)
            OreTutorial();
    }
}
