using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessSystem : MonoBehaviour
{
    // Private Attributes
    private PlayerLightChecker playerLightChecker;
    private bool playerInLight;
    private List<GameObject> enemySpawners = new List<GameObject>();

    private int ENEMY_CAP = 4;
    private int numberOfAliveEnemies = 0;
    public bool enemyCapIsFull = false;

    public delegate void PlayerEntersLightAction();
    public static event PlayerEntersLightAction OnPlayerEntersLight;
    public static event PlayerEntersLightAction OnPlayerNotInLight;

    void Start()
    {
        playerLightChecker = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLightChecker>();
        playerInLight = playerLightChecker.IsPlayerInLight();

        enemySpawners = new List<GameObject>(GameObject.FindGameObjectsWithTag("EnemySpawner"));
    }

    void Update()
    {
        if (enemyCapIsFull)
        {
            DisableEnemySpawners();
            return;
        }
        else if (!playerInLight)
        {
            EnableEnemySpawners();
        }



        if (!playerInLight && playerLightChecker.IsPlayerInLight())
        {
            playerInLight = true;
            //DisableEnemySpawners();
            if(OnPlayerEntersLight != null)
            {
                OnPlayerEntersLight();
            }
        }
        else if (playerInLight && !playerLightChecker.IsPlayerInLight())
        {
            playerInLight = false;
            //EnableEnemySpawners();
            if (OnPlayerNotInLight != null) OnPlayerNotInLight();
        }
    }


    void OnEnable()
    {
        EnemySpawner.spawnEnemyEvent += AddingEnemy;
        //HostileEnemy.enemyDisappearsEvent += RemovingEnemy;
        EnemyDestroyState.OnEnemyDestroy += RemovingEnemy;
    }

    void OnDisable()
    {
        EnemySpawner.spawnEnemyEvent -= AddingEnemy;
        //HostileEnemy.enemyDisappearsEvent -= RemovingEnemy;
        EnemyDestroyState.OnEnemyDestroy -= RemovingEnemy;
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
    private void AddingEnemy()
    {
        numberOfAliveEnemies++;
        enemyCapIsFull = numberOfAliveEnemies >= ENEMY_CAP;
    }

    private void RemovingEnemy()
    {
        numberOfAliveEnemies--;
        enemyCapIsFull = numberOfAliveEnemies >= ENEMY_CAP;
    }
}