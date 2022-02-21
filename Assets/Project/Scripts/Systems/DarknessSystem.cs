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
    private bool spawnersEnabledAlready = false;
    private bool spawnersDisabledAlready = false;

    private bool isDuringLightEnterDelay = false;
    private bool isDuringLightExitDelay = false;

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
            //if (!spawnersDisabledAlready) 
            DisableEnemySpawners();
        }
        else
        {
            //if (!spawnersEnabledAlready) 
            EnableEnemySpawners();
        }
        //else if (!playerInLight)
        //{
        //    EnableEnemySpawners();
        //}



        if (IsPlayerEnteringLight())
        {
            //playerInLight = true;
            ////DisableEnemySpawners();
            //if(OnPlayerEntersLight != null)
            //{
            //    OnPlayerEntersLight();
            //}
            if (!isDuringLightEnterDelay) StartCoroutine(DelayOnPlayerInLight());
        }
        else if (IsPlayerExitingLight())
        {
            //playerInLight = false;
            ////EnableEnemySpawners();
            //if (OnPlayerNotInLight != null) OnPlayerNotInLight();

            if (!isDuringLightExitDelay) StartCoroutine(DelayOnPlayerNotInLight());
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
        spawnersDisabledAlready = false;
        spawnersEnabledAlready = true;

        for (int i = 0; i < enemySpawners.Count; i++)
        {
            enemySpawners[i].SetActive(true);
        }
    }

    private void DisableEnemySpawners()
    {
        spawnersDisabledAlready = true;
        spawnersEnabledAlready = false;

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


    private bool IsPlayerEnteringLight()
    {
        return !playerInLight && playerLightChecker.IsPlayerInLight();
    }

    IEnumerator DelayOnPlayerInLight()
    {
        isDuringLightEnterDelay = true;
        yield return new WaitForSeconds(0.05f);

        isDuringLightEnterDelay = false;
        if (/*IsPlayerEnteringLight()*/ !isDuringLightExitDelay)
        {
            playerInLight = true;
            //DisableEnemySpawners();
            if (OnPlayerEntersLight != null)
            {
                OnPlayerEntersLight();
            }
        }
    }

    private bool IsPlayerExitingLight()
    {
        return playerInLight && !playerLightChecker.IsPlayerInLight();
    }

    IEnumerator DelayOnPlayerNotInLight()
    {
        isDuringLightExitDelay = true;
        yield return new WaitForSeconds(0.05f);

        isDuringLightExitDelay = false;
        if (/*IsPlayerExitingLight()*/ !isDuringLightEnterDelay)
        {
            playerInLight = false;
            //EnableEnemySpawners();
            if (OnPlayerNotInLight != null) OnPlayerNotInLight();
        }
    }


}