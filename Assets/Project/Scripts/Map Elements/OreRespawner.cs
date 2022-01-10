using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreRespawner : Spawner
{
    // Private Attributes
    private const float TIME_BETWEEN_RESPAWN = 5f;

    private Transform respawnTransform;
    private bool canSpawn = false;

    // Public Attributes
    public GameObject oreToRespawn;

    private void Start()
    {
        respawnTransform = transform;
        SetSpawnCooldown(TIME_BETWEEN_RESPAWN);
    }

    protected override void Spawn()
    {
        Instantiate(oreToRespawn, respawnTransform);
    }
}
