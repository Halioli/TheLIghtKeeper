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

    //public int[] inventoryItemQuantity;
    //public int[] inventoryItemID;

    public bool[] luxiniteMined;

    public bool[] constructedBridges;

    public bool[] oreMined;

    public bool[] lightFisuresActive;
    public bool[] oreVeinActivated;

    public bool[] geckosDead;

    public int[] upgrades;


    public PlayerData(GameObject player, int sizeTeleports, GameObject cam, int sizeTorch, int sizeLuxinites, 
        int sizeOres, int sizeBridges/*, int sizeUpgrades, Dictionary<int, int> inventoryData*/, 
        int sizeGeckos, int sizeLightFisures, int sizeOreVeins)
    {
        // == PLAYER ==
        playerPos = new float[3];
        playerPos[0] = player.transform.position.x;
        playerPos[1] = player.transform.position.y;
        playerPos[2] = player.transform.position.z;

        // == CAMERA ==
        cameraPos = new float[3];
        cameraPos[0] = cam.transform.position.x;
        cameraPos[1] = cam.transform.position.y;
        cameraPos[2] = cam.transform.position.z;

        // == HEALTH ==
        health = player.GetComponent<HealthSystem>().GetHealth();

        // == LAMP ==
        lampTime = player.GetComponentInChildren<Lamp>().GetLampTimeRemaining();
        activeLamp = player.GetComponentInChildren<Lamp>().active;
        coneActiveLamp = player.GetComponentInChildren<Lamp>().coneIsActive;

        enableTeleport = new bool[sizeTeleports];
        torchTurnedOn = new bool[sizeTorch];

        luxiniteMined = new bool[sizeLuxinites];

        lightFisuresActive = new bool[sizeLightFisures];
        oreVeinActivated = new bool[sizeOreVeins];

        //inventoryItemQuantity = new int[inventoryData.Count];
        //inventoryItemID = new int[inventoryData.Count];

        //int i = 0;
        //foreach (KeyValuePair<int, int> inventoryPair in inventoryData)
        //{
        //    inventoryItemQuantity[i] = inventoryPair.Value;
        //    inventoryItemID[i] = inventoryPair.Key;
        //    i++;
        //}

        constructedBridges = new bool[sizeBridges];

        oreMined = new bool[sizeOres];

        geckosDead = new bool[sizeGeckos];

        //upgrades = new int[sizeUpgrades];
    }

}
