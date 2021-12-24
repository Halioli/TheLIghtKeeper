using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAreas : MonoBehaviour
{
    // Private Attributes
    private const float DEFAULT_Y = -1f;
    private const float RIGHT = 1.25f;
    private const float LEFT = -1.25f;
    private const float TOP = 0.25f;
    private const float DOWN = -2.25f;
    private const float ROTATION = 90f;

    private Vector2 playerMovement;
    private Vector3 interactAreaPosition;
    private Vector3 attackAreaPosition;
    private Quaternion attackAreaRotation;

    // Public Attributes
    public GameObject interactArea;
    public GameObject attackArea;

    private void Start()
    {
        playerMovement = PlayerInputs.instance.PlayerPressedMovementButtons();
        interactAreaPosition = interactArea.transform.localPosition;
        attackAreaPosition = attackArea.transform.localPosition;
        attackAreaRotation = attackArea.transform.rotation;
    }

    private void Update()
    {
        playerMovement = PlayerInputs.instance.PlayerPressedMovementButtons();

        if (playerMovement.y != 0)
        {
            interactAreaPosition.x = 0f;
            attackAreaPosition.x = 0f;
            attackAreaRotation.Set(0f, 0f, 0f, 0f);

            if (playerMovement.y > 0)
            {
                interactAreaPosition.y = TOP;
                attackAreaPosition.y = TOP;
            }
            else if (playerMovement.y < 0)
            {
                interactAreaPosition.y = DOWN;
                attackAreaPosition.y = DOWN;
            }

            UpdateAreas();
        }
        else if (playerMovement.x != 0 && playerMovement.y == 0)
        {
            interactAreaPosition.y = DEFAULT_Y;
            attackAreaPosition.y = DEFAULT_Y;
            attackAreaRotation.Set(0f, 0f, ROTATION, 0f);

            if (playerMovement.x > 0)
            {
                interactAreaPosition.x = RIGHT;
                attackAreaPosition.x = RIGHT;
            }
            else if (playerMovement.x < 0)
            {
                interactAreaPosition.x = LEFT;
                attackAreaPosition.x = LEFT;
            }

            Debug.Log(attackAreaRotation.z);
            UpdateAreas();
        }
    }

    private void UpdateAreas()
    {
        interactArea.transform.localPosition = interactAreaPosition;
        attackArea.transform.localPosition = attackAreaPosition;
        attackArea.transform.rotation = attackAreaRotation;
    }
}
