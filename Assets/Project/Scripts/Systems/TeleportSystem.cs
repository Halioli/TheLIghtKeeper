using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSystem : MonoBehaviour
{
    public List<GameObject> teleports;
    private Vector3 teleportToGoPosition;

    void Start()
    {
        //teleports = new List<GameObject>(GameObject.FindGameObjectsWithTag("Teleporter"));
    }

    private void TeleportPlayer()
    {

    }
}
