using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
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
            strm.Close();
            return playerData;

        }
        else
        {
            Debug.LogError("Save file not exists " + path);
            return null;
        }
    }

    public static void SaveTeleporters(Teleporter teleport)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "teleport.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        TeleportData data = new TeleportData(teleport);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Saved");
    }

    public static TeleportData LoadTeleporters()
    {
        string path = Application.persistentDataPath + "teleport.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream strm = new FileStream(path, FileMode.Open);

            TeleportData teleportData = formatter.Deserialize(strm) as TeleportData;
            strm.Close();
            return teleportData;

        }
        else
        {
            Debug.LogError("Save file not exists " + path);
            return null;
        }
    }
}
