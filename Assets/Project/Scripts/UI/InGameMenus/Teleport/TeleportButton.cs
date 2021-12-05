using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportButton : MonoBehaviour
{
    private GameObject player;
    private TeleportSystem teleportSystem;
    public int buttonNumber;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        teleportSystem = GameObject.FindGameObjectWithTag("TeleportSystem").GetComponent<TeleportSystem>();
    }

    public void TeleportToLocation()
    {
        player.transform.position = teleportSystem.teleports[buttonNumber].GetComponent<Teleporter>().teleportTransformPosition;
    }
}
