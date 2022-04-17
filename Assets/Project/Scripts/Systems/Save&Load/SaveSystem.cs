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
    public static int[] upgradesLevels;

    public static GameObject player;
    public static GameObject cam;
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
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        playerHealthSystem = player.GetComponent<HealthSystem>();
        playerLamp = player.GetComponentInChildren<Lamp>();
        furnace = GameObject.FindGameObjectWithTag("Furnace");
        playerInventory = player.GetComponentInChildren<Inventory>();

        if (!firstTime)
        {
            LoadPlayerOnStart();
        }
    }

    private void OnApplicationQuit()
    {
        SavePlayerData(player, cam, furnace);
    }

    public void SavePlayerData(GameObject player, GameObject camera, GameObject furnace)
    {
        upgradesLevels = upgradeMenuCanvas.GetAllUpgardesLastActiveButtonIndex();

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "player.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player, teleporters.Count, camera, torches.Count, furnace, 
                                playerInventory.GetInventoryData(), luxinites.Count, bridges.Count, ores.Count, upgradesLevels.Length);

        for (int i = 0; i < torches.Count; i++)
        {
            data.torchTurnedOn[i] = torches[i].turnedOn;
        }

        for (int i = 0; i < teleporters.Count; i++)
        {
            data.enableTeleport[i] = teleporters[i].activated;
        }

        for(int i = 0; i < luxinites.Count; i++)
        {
            data.luxiniteMined[i] = luxinites[i].hasBeenMined;
        }

        for (int i = 0; i < bridges.Count; i++)
        {
            data.constructedBridges[i] = bridges[i].constructed;
        }

        for (int i = 0; i < ores.Count; i++)
        {
            data.oreMined[i] = ores[i].hasBeenMined;
        }

        for (int i = 0; i < upgradesLevels.Length; i++)
        {
            data.upgrades[i] = upgradesLevels[i];
        }

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Saved " + data.lampTime);
    }

    public static PlayerData LoadPlayerOnStart()
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

            furnace.GetComponent<Furnace>().lightLevel = playerData.furnaceLevel;

            for (int i = 0; i < teleporters.Count; i++)
            {
                teleporters[i].activated = playerData.enableTeleport[i];
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

            for(int i = 0; i < playerData.inventoryItemID.Length; i++)
            {
                playerInventory.AddNItemsToInventory(ItemLibrary.instance.GetItem(playerData.inventoryItemID[i]), playerData.inventoryItemQuantity[i]);
            }

            for(int i = 0; i < luxinites.Count; i++)
            {
                luxinites[i].hasBeenMined = playerData.luxiniteMined[i];

                if (luxinites[i].hasBeenMined)
                {
                    luxinites[i].gameObject.SetActive(false);
                }
            }

            for (int i = 0; i < luxinites.Count; i++)
            {
                ores[i].hasBeenMined = playerData.oreMined[i];

                if (ores[i].hasBeenMined)
                {
                    ores[i].gameObject.SetActive(false);
                }
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

