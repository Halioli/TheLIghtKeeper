using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class TeleportData
{
    public bool enable;
    public TeleportData(Teleporter teleport)
    {
        enable = teleport.activated;
    }
}
