using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportData
{
    public GameObject[] teleports = GameObject.FindGameObjectsWithTag("Teleporter");
    public Teleporter[] teleporters;
    public bool[] enable;
    public TeleportData(Teleporter teleport)
    {
        int i = 0;
        foreach(GameObject telept in teleports) 
        {
            teleporters[i] = telept.GetComponent<Teleporter>();
            enable[i] = teleporters[i].activated;
            i++;
        }
    }
}
