using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageItemToStorage : TutorialMessages
{
    private void Awake()
    {
        DisableSelf();
    }

    private void OnEnable()
    {
        CraftingStation.OnItemSentToStorage += SendMessage;
    }

    private void OnDisable()
    {
        CraftingStation.OnItemSentToStorage -= SendMessage;
    }

    //protected override void SendMessage()
    //{
    //    base.SendMessage();

    //    Debug.Log(mssg);
    //}
}
