using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessSystem : MonoBehaviour
{
    // Private Attributes
    private PlayerLightChecker playerLightChecker;
    private bool playerInLight;
    private List<GameObject> enemySpawners = new List<GameObject>();
    
    void Start()
    {
        playerLightChecker = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLightChecker>();
        playerInLight = playerLightChecker.IsPlayerInLight();

        enemySpawners = new List<GameObject>(GameObject.FindGameObjectsWithTag("EnemySpawner"));
    }

    void Update()
    {
        if (!playerInLight && playerLightChecker.IsPlayerInLight())
        {
            playerInLight = true;
            DisableEnemySpawners();
            MakeAllEnemiesBanish();
        }
        else if (playerInLight && !playerLightChecker.IsPlayerInLight())
        {
            playerInLight = false;
            EnableEnemySpawners();
        }
    }

    private void EnableEnemySpawners()
    {
        for (int i = 0; i < enemySpawners.Count; i++)
        {
            enemySpawners[i].SetActive(true);
        }
    }

    private void DisableEnemySpawners()
    {
        for (int i = 0; i < enemySpawners.Count; i++)
        {
            enemySpawners[i].SetActive(false);
        }
    }

    private void MakeAllEnemiesBanish()
    {
        List<GameObject> spawnedEnemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            spawnedEnemies[i].GetComponent<Enemy>().Banish();
        }
    }

}
