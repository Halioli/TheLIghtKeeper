using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessSystem : MonoBehaviour
{
    // Private Attributes
    private PlayerLightChecker playerLightChecker;
    private bool playerInLight;

    // Public attributes
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
        }
        else if (playerInLight && !playerLightChecker.IsPlayerInLight()) 
        {
            playerInLight = false;
            EnableEnemySpawners();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            playerInLight = true;
            DisableEnemySpawners();
        }
        else if (Input.GetKeyDown(KeyCode.Y))
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

}
