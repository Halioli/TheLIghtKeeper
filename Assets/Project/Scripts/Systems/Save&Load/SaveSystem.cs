using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class SaveSystem : MonoBehaviour
{
    public static List<Teleporter> teleporters = new List<Teleporter>();
    public static List<Torch> torches = new List<Torch>();

    public static GameObject player;
    public static GameObject cam;
    public static HealthSystem playerHealthSystem;
    public static Lamp playerLamp;
    public static GameObject furnace;

    const string TP_COUNT_SUB = "/tp.count";

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        playerHealthSystem = player.GetComponent<HealthSystem>();
        playerLamp = player.GetComponentInChildren<Lamp>();
        furnace = GameObject.FindGameObjectWithTag("Furnace");
        LoadPlayer();
    }

    private void OnApplicationQuit()
    {
        SavePlayerData(player, cam, furnace);
    }

    public static void SavePlayerData(GameObject player, GameObject camera, GameObject furnace)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "player.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player, teleporters.Count, camera, torches.Count, furnace);

        for (int i = 0; i < torches.Count; i++)
        {
            data.torchTurnedOn[i] = torches[i].turnedOn;
        }

        for (int i = 0; i < teleporters.Count; i++)
        {
            data.enableTeleport[i] = teleporters[i].activated;
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

            strm.Close();
            return playerData;

        }
        else
        {
            Debug.Log("Save file not exists " + path);
            return null;
        }
    }

    public static void SetPlayerData(PlayerData playerData)
    {

    }
}

