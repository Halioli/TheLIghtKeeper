using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSystem : MonoBehaviour
{

    private Vector3 teleportToGoPosition;
    
    public List<GameObject> teleports;

    void Start()
    {
        //teleports = new List<GameObject>(GameObject.FindGameObjectsWithTag("Teleporter"));
    }

    private void TeleportPlayer()
    {

    }
}
