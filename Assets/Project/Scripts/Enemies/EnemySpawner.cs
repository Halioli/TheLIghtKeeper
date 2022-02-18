using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    // Private Attributes
    private GameObject playerGameObject;
    private Vector2 playerPosition;
    private bool spawnerIsActive;

    // Public Attributes
    public float spawnRadius = 2f;
    public float spawnerRadiusRange;
    public float enemySpawnCooldown;
    public List<GameObject> enemies;

    // Events
    public delegate void SpawnEnemy();
    public static event SpawnEnemy spawnEnemyEvent;


    private void Start()
    {
        spawnTimer = spawnCooldown;
        canSpawn = false;

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
            UpdatePlayerPosition();
            //playerPosition = playerGameObject.transform.position;
            spawnerIsActive = Vector2.Distance(playerPosition, transform.position) <= spawnerRadiusRange;
        }
    }


    // Methods to override
    protected override void Spawn()
    {
        InstantiateEnemy();
        spawnEnemyEvent();
    }

    // Methods
    private void UpdatePlayerPosition()
    {
        playerPosition = playerGameObject.transform.position;
    }

    private void InstantiateEnemy()
    {
        GameObject newEnemy = Instantiate(GetRandomEnemyFromList(), (Vector2)transform.position + Random.insideUnitCircle * spawnRadius, Quaternion.identity);
        newEnemy.GetComponent<EnemyMonster>().SetPlayer(playerGameObject);
    }

    private GameObject GetRandomEnemyFromList()
    {
        int randomNumber = Random.Range(0, enemies.Count - 1);
        return enemies[randomNumber];
    }



}