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
    public List<Enemy> enemies;



    private void Start()
    {
        playerGameObject = GameObject.FindGameObjectsWithTag("Player")[0];
        UpdatePlayerPosition();
    }

    private void Update()
    {
        if (spawnerIsActive)
        {
            SpawnLogic();
        }
        else
        {
            // Check player inside spawner range
        }
    }




    // Methods
    private void UpdatePlayerPosition()
    {
        playerPosition = playerGameObject.transform.position;
    }

    private void InstantiateEnemy()
    {
        Instantiate(GetRandomEnemyFromList());
    }

    private Enemy GetRandomEnemyFromList()
    {
        int randomNumber = Random.Range(0, enemies.Count - 1);
        return enemies[randomNumber];
    }



    // Methods to override
    protected override void Spawn()
    {
        InstantiateEnemy();
    }
}
