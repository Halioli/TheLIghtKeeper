using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    // Public Attributes
    private HealthSystem playerHealthSystem;
    private Rigidbody2D playerRigidbody2D;

    private void Start()
    {
        playerHealthSystem = GetComponent<HealthSystem>();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (playerHealthSystem.IsDead())
        {
            // Teleport to starting position (0, 0)
            playerRigidbody2D.transform.position = Vector3.zero;
            playerHealthSystem.RevivePlayer();
        }
    }
}
