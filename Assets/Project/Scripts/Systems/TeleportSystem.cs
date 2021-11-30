using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSystem : MonoBehaviour
{
    private List<GameObject> teleports;
    private Vector2 teleportToGo;

    // Start is called before the first frame update
    void Start()
    {
        teleports = new List<GameObject>(GameObject.FindGameObjectsWithTag("Teleporter"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TeleportPlayer()
    {

    }
}
