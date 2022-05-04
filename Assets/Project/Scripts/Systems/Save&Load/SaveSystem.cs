using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class SaveSystem : MonoBehaviour
{
    public static List<Teleporter> teleporters = new List<Teleporter>();
    public static List<Torch> torches = new List<Torch>();
    public static List<Luxinite> luxinites = new List<Luxinite>();
    public static List<BridgeManager> bridges = new List<BridgeManager>();
    public static List<Ore> ores = new List<Ore>();
    public static List<Gecko> geckos = new List<Gecko>();

    public static int[] upgradesLevels;

    public GameObject player;
    public GameObject cam;
    public static HealthSystem playerHealthSystem;
    public static Lamp playerLamp;
    public static GameObject furnace;
    public static Inventory playerInventory;
    public UpgradeMenuCanvas upgradeMenuCanvas;

    public static bool firstTime;

    private void Awake()
    {
        firstTime = PlayerPrefs.GetInt("FirstTime") == 1;
        if(!firstTime)
        {
            LoadPlayerOnAwake();
        }
    }
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //cam = GameObject.FindGameObjectWithTag("MainCamera");
        playerHealthSystem = player.GetComponent<HealthSystem>();
        playerLamp = player.GetComponentInChildren<Lamp>();
        playerInventory = player.GetComponentInChildren<Inventory>();

        if (!firstTime)
        {
            LoadPlayerOnStart();
        }
    }

    private void Update()
    {
        SaveKeyEvent();
    }
    
    private void OnApplicationQuit()
    {
        SavePlayerData(player, cam);
    }

    private void SaveKeyEvent()
    {
       if(PlayerInputs.instance.PlayerPressedPauseButton())
        {
            SavePlayerData(player, cam);
        }
    }

    public void SavePlayerData(GameObject player, GameObject camera)
    {
        upgradesLevels = upgradeMenuCanvas.GetAllUpgardesLastActiveButtonIndex();

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "player.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player, teleporters.Count, camera,torches.Count, luxinites.Count, ores.Count, bridges.Count, upgradesLevels.Length, playerInventory.GetInventoryData(), geckos.Count);

        Debug.Log("Player: " + player);
        foreach(Teleporter tp in teleporters)
        {
            Debug.Log("Teleports: " + tp.activated);
        }

        foreach (Torch torch in torches)
        {
            Debug.Log("Torches: " + torch.turnedOn);
        }

        for (int i = 0; i < torches.Count; i++)
        {
            data.torchTurnedOn[i] = torches[i].turnedOn;
        }

        for (int i = 0; i < teleporters.Count; i++)
        {
            data.enableTeleport[i] = teleporters[i].activated;
        }

        for (int i = 0; i < luxinites.Count; i++)
        {
            data.luxiniteMined[i] = luxinites[i].hasBeenMined;
            Debug.Log("Luxinites: " + luxinites[i].hasBeenMinedLux);
        }

        for (int i = 0; i < bridges.Count; i++)
        {
            data.constructedBridges[i] = bridges[i].constructed;
            Debug.Log("Bridges: " + bridges[i].constructed);

        }

        for (int i = 0; i < ores.Count; i++)
        {
            data.oreMined[i] = ores[i].hasBeenMined;
            Debug.Log("Ores: " + ores[i].hasBeenMined);
        }

        for (int i = 0; i < upgradesLevels.Length; i++)
        {
            data.upgrades[i] = upgradesLevels[i];
        }

        for (int i = 0; i < geckos.Count; i++)
        {
            data.geckosDead[i] = geckos[i].geckoDead;
        }

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Saved lamptime: " + data.lampTime);
    }

    public PlayerData LoadPlayerOnStart()
    {
        string path = Application.persistentDataPath + "player.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream strm = new FileStream(path, FileMode.Open);

            PlayerData playerData = formatter.Deserialize(strm) as PlayerData;

            playerHealthSystem.health = playerData.health;

            Vector3 position;
            position.x = playerData.playerPos[0];
            position.y = playerData.playerPos[1];
            position.z = playerData.playerPos[2];
            player.transform.position = position;

            position.x = playerData.cameraPos[0];
            position.y = playerData.cameraPos[1];
            position.z = playerData.cameraPos[2];
            cam.transform.position = position;

            for (int i = 0; i < ores.Count; i++)
            {
                ores[i].hasBeenMined = playerData.oreMined[i];
            }

            for (int i = 0; i < torches.Count; i++)
            {
                torches[i].turnedOn = playerData.torchTurnedOn[i];

                if (playerData.torchTurnedOn[i])
                {
                    torches[i].SetTorchLightOn();
                }
                    //else
                    //{
                    //    torches[i].SetTorchLightOff();
                    //}
            }

            for(int i = 0; i < geckos.Count; i++)
            {
                geckos[i].geckoDead = playerData.geckosDead[i];
            }

            for (int i = 0; i < teleporters.Count; i++)
            {
                teleporters[i].activated = playerData.enableTeleport[i];
                if (teleporters[i].activated)
                {
                    teleporters[i].teleportSpriteRenderer.sprite = teleporters[i].teleportActivatedSprite;
                    teleporters[i].animator.SetBool("isActivated", true);
                    teleporters[i].animator.Play("Ends", 0, 0.25f);
                }

            }

            for (int i = 0; i < bridges.Count; i++)
            {
                bridges[i].constructed = playerData.constructedBridges[i];
            }

            playerLamp.lampTime = playerData.lampTime;
            playerLamp.active = playerData.activeLamp;
            playerLamp.turnedOn = playerData.coneActiveLamp;

            if (playerData.coneActiveLamp)
            {
                playerLamp.ActivateConeLight();
            }
            if(playerData.activeLamp)
            {
                playerLamp.ActivateCircleLight();
            }

            for (int i = 0; i < playerData.inventoryItemID.Length; i++)
            {
                playerInventory.AddNItemsToInventory(ItemLibrary.instance.GetItem(playerData.inventoryItemID[i]), playerData.inventoryItemQuantity[i]);
            }

            strm.Close();
            return playerData;

        }
        else
        {
            Debug.Log("Save file not exists " + path);
            return null;
        }
    }

    public PlayerData LoadPlayerOnAwake()
    {
        string path = Application.persistentDataPath + "player.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream strm = new FileStream(path, FileMode.Open);

            PlayerData playerData = formatter.Deserialize(strm) as PlayerData;

            upgradeMenuCanvas.SetAllLastCompletedButtonIndex(playerData.upgrades);
            
            for (int i = 0; i < luxinites.Count; i++)
            {
                luxinites[i].hasBeenMinedLux = playerData.luxiniteMined[i];
                if (luxinites[i].hasBeenMinedLux)
                {
                    luxinites[i].gameObject.SetActive(false);
                }
            }
            //Debug.Log(player.transform.position.x);
            strm.Close();
            return playerData;
        }
        else
        {
            Debug.Log("Save file not exists " + path);
            return null;
        }
    }
}

