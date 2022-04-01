using UnityEngine;


[System.Serializable]
public class MessegeData
{
    public enum MessegeEventType { NONE, TUTORIAL, CAMERA };


    [System.Serializable]
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



    [TextArea(5, 20)] public string messege;
    public ChatEventData chatEventData;

}
