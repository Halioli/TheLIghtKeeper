using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMessages : MonoBehaviour
{
    public delegate void OpenChatBox(string mssg);
    public static event OpenChatBox OnNewMessage;

    [TextArea(5, 20)] public string mssg;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SendMessage();
        }
    }

    protected virtual void SendMessage()
    {
        // Send Action
        if (OnNewMessage != null)
            OnNewMessage(mssg);

        DisableSelf();
    }

    protected void DisableSelf()
    {
        GetComponent<Collider2D>().enabled = false;
    }
}
