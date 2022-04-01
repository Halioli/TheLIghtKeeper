using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public float[] playerPos;
    public int health;

    public bool[] enableTeleport;

    public bool[] torchTurnedOn;
    
    public float lampTime;
    public bool lampTurnedOn;
    public bool activeLamp;
    public bool coneActiveLamp;

    public float[] cameraPos;

    public int furnaceLevel;

    public int[] inventoryItemQuantity;
    public int[] inventoryItemID;

    public bool[] luxiniteMined;
    public PlayerData(GameObject player, int sizeTeleports, GameObject cam, int sizeTorch, GameObject furnace, Dictionary<int,int> inventoryData, int sizeLuxinites)
    {
        playerPos = new float[3];
        playerPos[0] = player.transform.position.x;
        playerPos[1] = player.transform.position.y;
        playerPos[2] = player.transform.position.z;

        cameraPos = new float[3];
        cameraPos[0] = cam.transform.position.x;
        cameraPos[1] = cam.transform.position.y;
        cameraPos[2] = cam.transform.position.z;

        health = player.GetComponent<HealthSystem>().GetHealth();

        lampTime = player.GetComponentInChildren<Lamp>().GetLampTimeRemaining();
        activeLamp = player.GetComponentInChildren<Lamp>().active;
        coneActiveLamp = player.GetComponentInChildren<Lamp>().coneIsActive;

        enableTeleport = new bool[sizeTeleports];
        torchTurnedOn = new bool[sizeTorch];

        furnaceLevel = furnace.GetComponent<Furnace>().lightLevel;

        luxiniteMined = new bool[sizeLuxinites];

        inventoryItemQuantity = new int[inventoryData.Count];
        inventoryItemID = new int[inventoryData.Count];
        
        int i = 0;
        foreach(KeyValuePair<int,int> inventoryPair in inventoryData)
        {
            inventoryItemQuantity[i] = inventoryPair.Value;
            inventoryItemID[i] = inventoryPair.Key;
            i++;
        }
    }

}
