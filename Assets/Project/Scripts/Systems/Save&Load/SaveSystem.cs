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
    const string TP_COUNT_SUB = "/tp.count";

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerM = player.GetComponent<PlayerMovement>();
        LoadTeleporters();
        LoadPlayer();
    }

    private void OnApplicationQuit()
    {
        SaveTeleporters();
        SavePlayer(playerM);
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
            return playerData;

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

    void LoadTeleporters()
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
        }
        else
        {
            Debug.LogError("Save file not exists " + countPath);
        }

        for (int i = 0; i < teleporters.Count; i++)
        {
            if(File.Exists(path + i))
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
