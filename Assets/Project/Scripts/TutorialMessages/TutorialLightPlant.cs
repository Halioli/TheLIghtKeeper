using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLightPlant : TutorialMessages
{
    public delegate void TutorialLightPlantDone();
    public static event TutorialLightPlantDone DoTutorialLightPlant;

    private void OnEnable()
    {
        DoTutorialLightPlant += DisableSelf;
    }

    private void OnDisable()
    {
        DoTutorialLightPlant -= DisableSelf;
    }

    protected override void SendMessage()
    {
        base.SendMessage();

        // Send Action
        if (DoTutorialLightPlant != null)
            DoTutorialLightPlant();
    }
}
