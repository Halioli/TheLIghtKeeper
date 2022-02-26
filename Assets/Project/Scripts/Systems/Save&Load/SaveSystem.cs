using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class SaveSystem : MonoBehaviour
{
    public static List<Teleporter> teleporters = new List<Teleporter>();

    public static GameObject player;
    public static PlayerMovement playerM;
    public static HealthSystem playerHealthSystem;

    const string TP_COUNT_SUB = "/tp.count";

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerM = player.GetComponent<PlayerMovement>();
        playerHealthSystem = player.GetComponent<HealthSystem>();
        LoadTeleporters();
        LoadPlayer();
        LoadPlayerHealth();
    }

    private void OnApplicationQuit()
    {
        SaveTeleporters();
        SavePlayer(playerM);
        SavePlayerHealth(playerHealthSystem);
    }
    public static void SavePlayer(PlayerMovement player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "player.dat";

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Saved");
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "player.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream strm = new FileStream(path, FileMode.Open);

            PlayerData playerData = formatter.Deserialize(strm) as PlayerData;
            Vector3 position;
            position.x = playerData.playerPos[0];
            position.y = playerData.playerPos[1];
            position.z = playerData.playerPos[2];
            player.transform.position = position;
            strm.Close();
            Debug.Log("Player Loaded");
            return playerData;
        }
        else
        {
            Debug.LogError("Save file not exists " + path);
            return null;
        }
    }

    public static void SavePlayerHealth(HealthSystem playerHealthSystem)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "playerHealth.dat";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerHealthData data = new PlayerHealthData(playerHealthSystem);
        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Saved health " + data.health);
    }

    public static PlayerHealthData LoadPlayerHealth()
    {
        string path = Application.persistentDataPath + "playerHealth.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            
            FileStream strm = new FileStream(path, FileMode.Open);

            PlayerHealthData playerDataHealth = formatter.Deserialize(strm) as PlayerHealthData;
            playerHealthSystem.health = playerDataHealth.health;
            strm.Close();

            Debug.Log("Health Loaded " + playerHealthSystem.health);
            return playerDataHealth;

        }
        else
        {
            Debug.LogError("Save file not exists " + path);
            return null;
        }
    }

    public static void SaveTeleporters()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "teleport.dat";
        string countPath = Application.persistentDataPath + TP_COUNT_SUB;

        FileStream countStream = new FileStream(countPath, FileMode.Create);
        formatter.Serialize(countStream, teleporters.Count);
        countStream.Close();

        for (int i = 0; i < teleporters.Count; i++)
        {
            FileStream stream = new FileStream(path + i, FileMode.Create);
            TeleportData data = new TeleportData(teleporters[i]);
            formatter.Serialize(stream, data);
            stream.Close();

            if (teleporters[i].activated == true)
            {
                Debug.Log("Activated");
            }
            else
            {
                Debug.Log("Deactivated");
            }
        }
        Debug.Log("Saved");
    }

    public static void LoadTeleporters()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string countPath = Application.persistentDataPath + TP_COUNT_SUB;
        string path = Application.persistentDataPath + "teleport.dat";
        int teleporterCount = 0;

        if (File.Exists(countPath))
        {
            FileStream countStrm = new FileStream(countPath, FileMode.Open);

            teleporterCount = (int)formatter.Deserialize(countStrm);
            countStrm.Close();
            Debug.Log(teleporterCount);
        }
        else
        {
            Debug.LogError("Save file not exists " + countPath);
        }

        for (int i = 0; i < teleporters.Count; i++)
        {

            if (File.Exists(path + i))
            {

                FileStream stream = new FileStream(path + i, FileMode.Create);
                TeleportData data = formatter.Deserialize(stream) as TeleportData;
                stream.Close();

                teleporters[i].activated = data.enable;
                if(teleporters[i].activated == true)
                {
                    Debug.Log("Activated");
                }
                else
                {
                    Debug.Log("Deactivated");
                }
            }
            else
            {
                Debug.Log("path not found in " + path + i);
            }
        }
    }
}
