using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    // Private Attributes
    private GameObject playerGameObject;
    private Vector2 playerPosition;
    private float spawnerRadiusRange;
    private bool spawnerIsActive;

    // Public Attributes
    public float enemySpawnCooldown;
    public List<GameObject> enemies;


    private void Start()
    {
        spawnTimer = spawnCooldown;
        canSpawn = false;

        spawnerRadiusRange = 10f;
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        UpdatePlayerPosition();

        SetSpawnCooldown(enemySpawnCooldown);
    }

    private void Update()
    {
        if (spawnerIsActive)
        {
            SpawnLogic();
        }
        else
        {
            playerPosition = playerGameObject.transform.position;
            spawnerIsActive = Vector2.Distance(playerPosition, transform.position) <= spawnerRadiusRange;
        }
    }


    // Methods to override
    protected override void Spawn()
    {
        InstantiateEnemy();
    }

    // Methods
    private void UpdatePlayerPosition()
    {
        playerPosition = playerGameObject.transform.position;
    }

    private void InstantiateEnemy()
    {
        Instantiate(GetRandomEnemyFromList(), transform.position, Quaternion.identity);
    }

    private GameObject GetRandomEnemyFromList()
    {
        int randomNumber = Random.Range(0, enemies.Count - 1);
        return enemies[randomNumber];
    }



}
