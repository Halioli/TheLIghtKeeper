using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    // Public Attributes
    public HealthSystem playerHealthSystem;
    public Rigidbody2D playerRigidbody2D;

    void Update()
    {
        if (playerHealthSystem.IsDead())
        {
            // Teleport to starting position (0, 0)
            playerRigidbody2D.transform.position = Vector3.zero;
            playerHealthSystem.RevivePlayer();
        }
    }

    public void SetPlayerToStatic()
    {
        playerRigidbody2D.bodyType = RigidbodyType2D.Static;
    }

    public void SetPlayerToDynamic()
    {
        playerRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }
}
