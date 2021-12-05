using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSystem : MonoBehaviour
{
    private GameObject playerGameObject;
    public int currentTeleportInUse = 0;

    public List<Teleporter> teleports;
    private Dictionary<string, int> teleportIdentifier;



    private void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");

        teleportIdentifier = new Dictionary<string, int>();
        for (int i = 0; i < teleports.Count; ++i)
        {
            teleportIdentifier[teleports[i].teleportName] = i;
        }
        
    }


    private void OnEnable()
    {
        Teleporter.OnActivation += SetPlayerInCurrentTeleport;

        TeleportButton.OnSelection += TeleportPlayerToNewPosition;
    }

    private void OnDisable()
    {
        Teleporter.OnActivation -= SetPlayerInCurrentTeleport;
        
        TeleportButton.OnSelection -= TeleportPlayerToNewPosition;
    }


    private void SetPlayerInCurrentTeleport(string teleportName)
    {
        currentTeleportInUse = teleportIdentifier[teleportName];
    }

    private void TeleportPlayerToNewPosition(int teleportIndex)
    {
        playerGameObject.transform.position = teleports[teleportIndex].GetComponent<Teleporter>().teleportTransformPosition;

        PlayerInputs.instance.canMove = true;
    }
}
