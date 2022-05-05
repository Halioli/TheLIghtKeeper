using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMessageVein : TutorialMessages
{
    public delegate void VeinTutorialDone();
    public static event VeinTutorialDone VeinTutorial;

    private void OnEnable()
    {
        VeinTutorial += DisableSelf;
    }

    private void OnDisable()
    {
        VeinTutorial -= DisableSelf;
    }

    protected override void SendMessage()
    {
        base.SendMessage();

        // Send Action
        if (VeinTutorial != null)
            VeinTutorial();
    }
}
