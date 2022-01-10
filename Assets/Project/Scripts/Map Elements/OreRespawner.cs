using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreRespawner : Spawner
{
    // Private Attributes
    private const float OVERLAP_CIRCLE_RADIUS = 1f;

    private Vector3 respawnPosition;
    private Quaternion respawnRotation;
    private Collider2D returnedCollider;

    // Public Attributes
    public float timeBetweenRespawns = 2f;
    public GameObject oreToRespawn;
    public LayerMask defaultLayerMask;

    private void Start()
    {
        canSpawn = true;
        respawnPosition = gameObject.transform.position;
        respawnRotation = gameObject.transform.rotation;

        SetSpawnCooldown(timeBetweenRespawns);
        Spawn();
    }

    protected override void Spawn()
    {
        Instantiate(oreToRespawn, respawnPosition, respawnRotation);
        canSpawn = false;
        StartCoroutine(WaitForRespawn());
    }

    private void CheckIfCanSpawn()
    {
        returnedCollider = ReturnOverlapedColliders();

        if (returnedCollider.CompareTag("Ore"))
        {
            canSpawn = false;
        }
        else
        {
            canSpawn = true;
        }
    }

    private Collider2D ReturnOverlapedColliders()
    {
        return Physics2D.OverlapCircle(respawnPosition, OVERLAP_CIRCLE_RADIUS, defaultLayerMask);
    }

    IEnumerator WaitForRespawn()
    {
        do
        {
            yield return new WaitForSeconds(spawnCooldown);

            CheckIfCanSpawn();
            if (canSpawn)
            {
                Spawn();
            }

        } while (!canSpawn);
    }
}
