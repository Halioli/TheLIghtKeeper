using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTorchesMessage : TutorialMessages
{
    public delegate void TorchTutorialDone();
    public static event TorchTutorialDone OnTorchTutorialDone;

    private void OnEnable()
    {
        OnTorchTutorialDone += DisableSelf;
    }

    private void OnDisable()
    {
        OnTorchTutorialDone -= DisableSelf;
    }

    protected override void SendMessage()
    {
        base.SendMessage();

        // Send Action
        if (OnTorchTutorialDone != null)
            OnTorchTutorialDone();
    }
}
