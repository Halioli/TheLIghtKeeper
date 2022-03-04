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
                // Add game object to list so it doesn't repeat
                gameObjectsFound.Add(gameObjectsToFind[i]);

                // Send Action
                if (OnNewMessage != null)
                    OnNewMessage(i);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SendMessage(10);
        }
    }

    public void SendMessage(int mssgId)
    {
        // Send Action
        if (OnNewMessage != null)
            OnNewMessage(mssgId);
    }

    public void ResetAllTutorialMessages()
    {
        gameObjectsFound.Clear();
    }
}
