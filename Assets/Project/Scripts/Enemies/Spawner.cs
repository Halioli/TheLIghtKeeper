using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    // Protected Attributes
    protected float spawnCooldown;
    protected float spawnTimer;
    protected bool canSpawn;



    // Methods
    protected void SpawnLogic()
    {
        if (!canSpawn)
        {
            UpdateSpawnTimer();
        }
        else
        {
            Spawn();
            canSpawn = false;
        }
    }

    protected void SetSpawnCooldown(float spawnCooldownToSet)
    {
        spawnCooldown = spawnCooldownToSet;
    }

    protected void UpdateSpawnTimer()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0.0f)
        {
            canSpawn = true;
        }
    }

    // Each type of spawner needs to override Spawn();
    protected virtual void Spawn() { }


}
