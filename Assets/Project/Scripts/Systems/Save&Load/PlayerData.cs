using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public float[] playerPos;
    public PlayerData(PlayerMovement player)
    {
        playerPos = new float[3];
        playerPos[0] = player.transform.position.x;
        playerPos[1] = player.transform.position.y;
        playerPos[2] = player.transform.position.z;
    }
}

[System.Serializable]
public class PlayerHealthData
{
    public int health;
    public PlayerHealthData(HealthSystem playerHealthSystem)
    {
        health = playerHealthSystem.GetHealth();
    }
}
