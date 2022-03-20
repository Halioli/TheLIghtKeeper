using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStuckSpider : TutorialMessages
{
    public delegate void TutorialStuckSpiderDone();
    public static event TutorialStuckSpiderDone DoTutorialStuckSpider;

    private void OnEnable()
    {
        DoTutorialStuckSpider += DisableSelf;
    }

    private void OnDisable()
    {
        DoTutorialStuckSpider -= DisableSelf;
    }

    protected override void SendMessage()
    {
        base.SendMessage();

        // Send Action
        if (DoTutorialStuckSpider != null)
            DoTutorialStuckSpider();
    }
}
