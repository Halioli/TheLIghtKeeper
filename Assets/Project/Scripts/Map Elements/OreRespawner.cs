using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreRespawner : Spawner
{
    // Private Attributes
    private const float OVERLAP_CIRCLE_RADIUS = 1f;

    private bool playerInArea = false;
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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerInArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerInArea = false;
        }
    }

    protected override bool Spawn()
    {
        Instantiate(oreToRespawn, respawnPosition, respawnRotation);
        canSpawn = false;
        StartCoroutine(WaitForRespawn());

        return true;
    }

    private bool CheckIfCanSpawn()
    {
        returnedCollider = ReturnOverlapedColliders();

        if (returnedCollider.CompareTag("Ore") || playerInArea)
        {
            canSpawn = false;
            return false;
        }
        else
        {
            canSpawn = true;
            return true;
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

            if (CheckIfCanSpawn())
            {
                Spawn();
            }

        } while (!canSpawn);
    }
}
