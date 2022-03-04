using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class SaveSystem : MonoBehaviour
{
    public static List<Teleporter> teleporters = new List<Teleporter>();

    public static GameObject player;
    public static HealthSystem playerHealthSystem;
    public static Lamp playerLamp;

    const string TP_COUNT_SUB = "/tp.count";

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealthSystem = player.GetComponent<HealthSystem>();
        playerLamp = player.GetComponentInChildren<Lamp>();

        LoadPlayer();
    }

    private void OnApplicationQuit()
    {
        SavePlayerData(player);
    }

    public static void SavePlayerData(GameObject player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "player.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player, teleporters.Count);

        for (int i = 0; i < teleporters.Count; i++)
        {
            data.enable[i] = teleporters[i].activated;
        }
        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Saved " + data.lampTime);
    }

    public static PlayerData LoadPlayer()
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

            for (int i = 0; i < teleporters.Count; i++)
            {
                teleporters[i].activated = playerData.enable[i];
            }
            playerLamp.lampTime = playerData.lampTime;
            playerLamp.turnedOn = playerData.lampTurnedOn;
            playerLamp.active = playerData.activeLamp;
            playerLamp.turnedOn = playerData.coneActiveLamp;


            strm.Close();
            return playerData;

        }
        else
        {
            Debug.LogError("Save file not exists " + path);
            return null;
        }
    }

    public static void SetPlayerData(PlayerData playerData)
    {

    }
}

