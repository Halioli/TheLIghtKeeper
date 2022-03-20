using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageItemToStorage : TutorialMessages
{
    private void OnEnable()
    {
        CraftingSystem.OnItemSentToStorage += SendMessage;
    }

    private void OnDisable()
    {
        CraftingSystem.OnItemSentToStorage -= SendMessage;
    }

    //protected override void SendMessage()
    //{
    //    base.SendMessage();

    //    Debug.Log(mssg);
    //}
}
