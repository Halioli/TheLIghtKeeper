using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMessages : MonoBehaviour
{

    public enum MessegeEventType { NONE, TUTORIAL, CAMERA };

    
    public struct ChatEventData
    {
        public ChatEventData(int eventIndex, MessegeEventType eventType, int eventID)
        {
            this.eventIndex = eventIndex;
            this.eventType = eventType;
            this.eventID = eventID;

        }

        public int eventIndex;
        public MessegeEventType eventType;
        public int eventID;
    }



    public delegate void OpenChatBox(string[] mssg, ChatEventData chatEventData);
    public static event OpenChatBox OnNewMessage;

    [SerializeField] private int eventIndex = -1;
    [SerializeField] private MessegeEventType eventType = MessegeEventType.NONE;
    [SerializeField] private int eventID = -1;
    //[SerializeField] private int[] eventIndex;
    //[SerializeField] private MessegeEventType[] eventType;
    //[SerializeField] private int[] eventID;
    //[SerializeField] MessegeData[] messeges;

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
            OnNewMessage(mssgs, new ChatEventData(eventIndex, eventType, eventID));

        DisableSelf();
    }

    protected void DisableSelf()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    protected void EnableSelf()
    {
        GetComponent<Collider2D>().enabled = true;
    }
}
