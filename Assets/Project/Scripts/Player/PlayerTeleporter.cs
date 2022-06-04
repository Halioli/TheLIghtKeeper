using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform mainCameraTransform;

    private void OnEnable()
    {
        PlayerHandler.OnTeleportPlayer += TeleportToPosition;
        FogSystem.OnTeleportPlayer += TeleportToPosition;
        TeleportSystem.OnTeleportPlayer += TeleportToPosition;

        DarknessFaint.OnFaintTeleport += TeleportToPosition;
        
        Hotkeys.OnCheatTeleportFogZone += TeleportToPosition;
    }

    private void OnDisable()
    {
        PlayerHandler.OnTeleportPlayer -= TeleportToPosition;
        FogSystem.OnTeleportPlayer -= TeleportToPosition;
        TeleportSystem.OnTeleportPlayer -= TeleportToPosition;

        DarknessFaint.OnFaintTeleport -= TeleportToPosition;
        
        Hotkeys.OnCheatTeleportFogZone -= TeleportToPosition;
    }

    private void TeleportToPosition(Vector3 position)
    {
        playerTransform.position = position;

        position.z = -10f;
        mainCameraTransform.position = position;
    }

    private void TeleportToZero()
    {
        playerTransform.position = Vector3.zero;
        mainCameraTransform.position = new Vector3(0, 0, -10);
    }


}
