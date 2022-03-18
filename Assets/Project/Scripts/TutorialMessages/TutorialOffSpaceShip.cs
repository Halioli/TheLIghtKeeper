using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialOffSpaceShip : TutorialMessages
{
    public delegate void TutorialOffSpaceShipDone();
    public static event TutorialOffSpaceShipDone DoTutorialOffSpaceShip;

    private void OnEnable()
    {
        DoTutorialOffSpaceShip += DisableSelf;
    }

    private void OnDisable()
    {
        DoTutorialOffSpaceShip -= DisableSelf;
    }

    protected override void SendMessage()
    {
        base.SendMessage();

        // Send Action
        if (DoTutorialOffSpaceShip != null)
            DoTutorialOffSpaceShip();
    }
}