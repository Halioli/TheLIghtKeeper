using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSystem : MonoBehaviour
{
    private Dictionary<string, int> teleportIdentifier;
    private int numberOfActiveTeleports = 0;
    private bool sentMessage = false;

    public GameObject playerGameObject;
    public int currentTeleportInUse = 0;
    public List<Teleporter> teleports;

    // Tp player
    public delegate void TeleportPlayerAction(Vector3 landingPos);
    public static event TeleportPlayerAction OnTeleportPlayer;

    public delegate void SecondTeleportActiveAction();
    public static event SecondTeleportActiveAction OnSecondTeleportActive;

    private void Start()
    {
        teleportIdentifier = new Dictionary<string, int>();

        for (int i = 0; i < teleports.Count; ++i)
        {
            teleportIdentifier[teleports[i].teleportName] = i;
        }
    }

    private void Update()
    {
        if (!sentMessage)
        {
            for (int i = 0; i < teleports.Count; i++)
            {
                if (teleports[i].activated)
                {
                    numberOfActiveTeleports++;
                }

                if (numberOfActiveTeleports >= 2)
                {
                    if (OnSecondTeleportActive != null)
                        OnSecondTeleportActive();

                    sentMessage = true;
                    i = teleports.Count;
                }
            }
            numberOfActiveTeleports = 0;
        }
    }

    private void OnEnable()
    {
        Teleporter.OnActivation += SetPlayerInCurrentTeleport;
        Teleporter.OnInteraction += SetPlayerInCurrentTeleport;

        TeleportButton.OnSelection += TeleportPlayerToNewPosition;
    }

    private void OnDisable()
    {
        Teleporter.OnActivation -= SetPlayerInCurrentTeleport;
        Teleporter.OnInteraction -= SetPlayerInCurrentTeleport;
        
        TeleportButton.OnSelection -= TeleportPlayerToNewPosition;
    }


    private void SetPlayerInCurrentTeleport(string teleportName)
    {
        currentTeleportInUse = teleportIdentifier[teleportName];
    }

    private void TeleportPlayerToNewPosition(int teleportIndex)
    {
        //playerGameObject.transform.position = teleports[teleportIndex].GetComponent<Teleporter>().teleportTransformPosition;
        if (OnTeleportPlayer != null)
        {
            OnTeleportPlayer(teleports[teleportIndex].GetComponent<Teleporter>().teleportTransformPosition);
        }

        PlayerInputs.instance.canMove = true;
    }
}
