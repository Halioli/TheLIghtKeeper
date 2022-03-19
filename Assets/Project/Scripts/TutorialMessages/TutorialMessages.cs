using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMessages : MonoBehaviour
{
    public delegate void OpenChatBox(string[] mssg, int eventIndex = -1);
    public static event OpenChatBox OnNewMessage;

    [SerializeField] private int eventIndex = -1;

    [TextArea(5, 20)] public string[] mssgs;

    public static bool tutorialOpened;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SendMessage();
        }
    }

    protected virtual void SendMessage()
    {
        tutorialOpened = true;

        // Send Action
        if (OnNewMessage != null)
            OnNewMessage(mssgs, eventIndex);

        DisableSelf();
    }

    protected void DisableSelf()
    {
        GetComponent<Collider2D>().enabled = false;
    }
}
