using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportButton : TeleportSystem
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void TeleportToLocation(int buttonNumb)
    {
        player.transform.position = teleports[buttonNumb].GetComponent<Teleporter>().teleportTransformPosition;
    }
}
