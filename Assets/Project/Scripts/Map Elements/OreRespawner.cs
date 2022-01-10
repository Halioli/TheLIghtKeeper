using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreRespawner : Spawner
{
    // Private Attributes
    private const float TIME_BETWEEN_RESPAWN = 5f;

    private Transform respawnTransform;

    // Public Attributes
    public GameObject oreToRespawn;

    private void Start()
    {
        canSpawn = true;
        respawnTransform = GetComponent<Transform>();

        SetSpawnCooldown(TIME_BETWEEN_RESPAWN);
        Spawn();
    }

    protected override void Spawn()
    {
        Instantiate(oreToRespawn, respawnTransform);
        canSpawn = false;
    }
}
