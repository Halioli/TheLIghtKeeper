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
    }

    private void OnDisable()
    {
        PlayerHandler.OnTeleportPlayer -= TeleportToPosition;
    }

    private void TeleportToPosition(Vector3 position)
    {
        playerTransform.position = position;
        mainCameraTransform.position = position;
    }
}
