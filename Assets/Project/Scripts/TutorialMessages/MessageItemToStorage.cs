using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageItemToStorage : TutorialMessages
{
    //public delegate void MessageItemToStorageDone();
    //public static event MessageItemToStorageDone DoItemToStorageMessage;

    private void OnEnable()
    {
        CraftingSystem.OnItemSentToStorage += SendMessage;
    }

    private void OnDisable()
    {
        CraftingSystem.OnItemSentToStorage -= SendMessage;
    }

    protected override void SendMessage()
    {
        Debug.Log("AAAAAAAAAAAAA");
        base.SendMessage();

        // Send Action
        //if (DoItemToStorageMessage != null)
        //    DoItemToStorageMessage();
    }
}
