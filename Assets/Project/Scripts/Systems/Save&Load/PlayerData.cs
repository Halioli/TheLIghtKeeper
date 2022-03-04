using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public float[] playerPos;
    public int health;

    public bool[] enable;

    public float lampTime;
    public bool lampTurnedOn;
    public bool activeLamp;
    public bool coneActiveLamp;

    public PlayerData(GameObject player, int size)
    {
        playerPos = new float[3];
        playerPos[0] = player.transform.position.x;
        playerPos[1] = player.transform.position.y;
        playerPos[2] = player.transform.position.z;

        health = player.GetComponent<HealthSystem>().GetHealth();

        lampTime = player.GetComponentInChildren<Lamp>().GetLampTimeRemaining();
        lampTurnedOn = player.GetComponentInChildren<Lamp>().turnedOn;
        activeLamp = player.GetComponentInChildren<Lamp>().active;
        coneActiveLamp = player.GetComponentInChildren<Lamp>().coneIsActive;
        enable = new bool[size];
    }

}
