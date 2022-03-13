using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessSystem : MonoBehaviour
{
    public static DarknessSystem instance;


    // Private Attributes
    private PlayerLightChecker playerLightChecker;
    public bool playerInLight { get; private set; }
    private List<GameObject> enemySpawners = new List<GameObject>();

    [SerializeField] int ENEMY_CAP = 4;
    private int numberOfAliveEnemies = 0;
    public bool enemyCapIsFull = false;
    private bool spawnersEnabledAlready = false;
    private bool spawnersDisabledAlready = false;

    private bool isDuringLightEnterDelay = false;
    private bool isDuringLightExitDelay = false;

    public delegate void PlayerEntersLightAction();
    public static event PlayerEntersLightAction OnPlayerEntersLight;
    public static event PlayerEntersLightAction OnPlayerNotInLight;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }



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
            playerInLight = true;
            //DisableEnemySpawners();
            InvokeOnPlayerEntersLight();
            //if (!isDuringLightEnterDelay) StartCoroutine(DelayOnPlayerInLight());
        }
        else if (IsPlayerExitingLight())
        {
            playerInLight = false;
            //EnableEnemySpawners();
            InvokeOnPlayerNotInLight();
            //if (!isDuringLightExitDelay) StartCoroutine(DelayOnPlayerNotInLight());
        }
    }


    void OnEnable()
    {
        EnemySpawner.spawnEnemyEvent += AddingEnemy;
        //HostileEnemy.enemyDisappearsEvent += RemovingEnemy;
        EnemyDestroyState.OnEnemyDestroy += RemovingEnemy;

        PlayerLightChecker.OnPlayerEntersLight += InvokeOnPlayerEntersLight;
    }

    void OnDisable()
    {
        EnemySpawner.spawnEnemyEvent -= AddingEnemy;
        //HostileEnemy.enemyDisappearsEvent -= RemovingEnemy;
        EnemyDestroyState.OnEnemyDestroy -= RemovingEnemy;
    }


    private void InvokeOnPlayerEntersLight()
    {
        if (OnPlayerEntersLight != null) OnPlayerEntersLight();
    }

    private void InvokeOnPlayerNotInLight()
    {
        if (OnPlayerNotInLight != null) OnPlayerNotInLight();
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
            InvokeOnPlayerEntersLight();
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
            InvokeOnPlayerNotInLight();
        }
    }


}