using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryFull_Message : TutorialMessages
{
    public delegate void InventoryFullTutorialDone();
    public static event InventoryFullTutorialDone OnInventoryFullTutorialDone;

    private void OnEnable()
    {
        OnInventoryFullTutorialDone += DisableSelf;
        HUDText.OnInventoryFull += SendMessage;
    }

    private void OnDisable()
    {
        OnInventoryFullTutorialDone -= DisableSelf;
        HUDText.OnInventoryFull -= SendMessage;
    }

    protected override void SendMessage()
    {
        base.SendMessage();

        // Send Action
        if (OnInventoryFullTutorialDone != null)
            OnInventoryFullTutorialDone();
    }
}
