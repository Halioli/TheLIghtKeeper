using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMetalPartsMessage : TutorialMessages
{
    public delegate void MetalPartTutorialDone();
    public static event MetalPartTutorialDone OnMetalPartTutorialDone;

    private void OnEnable()
    {
        OnMetalPartTutorialDone += DisableSelf;
    }

    private void OnDisable()
    {
        OnMetalPartTutorialDone -= DisableSelf;
    }

    protected override void SendMessage()
    {
        base.SendMessage();

        // Send Action
        if (OnMetalPartTutorialDone != null)
            OnMetalPartTutorialDone();
    }
}
