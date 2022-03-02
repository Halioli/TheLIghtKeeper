using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMessages : MonoBehaviour
{
    public List<GameObject> gameObjectsToFind;
    public List<GameObject> gameObjectsFound;

    public delegate void OpenChatBox(int mssgID);
    public static event OpenChatBox OnNewMessage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < gameObjectsToFind.Count; i++)
        {
            if (collision.CompareTag(gameObjectsToFind[i].tag) && 
                !gameObjectsFound.Contains(gameObjectsToFind[i]))
            {
                Debug.Log(gameObjectsToFind[i]);
                gameObjectsFound.Add(gameObjectsToFind[i]);

                // Send Action
                if (OnNewMessage != null)
                    OnNewMessage(i);
            }
        }
    }
}
