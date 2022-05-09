using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LorePilar_Message : TutorialMessages
{
    public delegate void LorePilarTutorialDone();
    public static event LorePilarTutorialDone OnLorePilarTutorialDone;

    private void OnEnable()
    {
        OnLorePilarTutorialDone += DisableSelf;

        LoreFunction.OnLorePilarActive += SendMessage;
    }

    private void OnDisable()
    {
        OnLorePilarTutorialDone -= DisableSelf;

        LoreFunction.OnLorePilarActive -= SendMessage;
    }

    protected override void SendMessage()
    {
        base.SendMessage();

        // Send Action
        if (OnLorePilarTutorialDone != null)
            OnLorePilarTutorialDone();
    }
}
