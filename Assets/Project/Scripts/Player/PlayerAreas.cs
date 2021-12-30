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
    private Vector3 mouseWorldPos;
    private Vector3 areaRotationOnHorizontal;

    //Attack
    private Vector3 attackAreaLeftPosition;
    private Vector3 attackAreaRightPosition;
    private Vector3 attackAreaTopPosition;
    private Vector3 attackAreaDownPosition;
    private Quaternion currentAttackAreaRotation;

    //Interact
    private Vector3 interactAreaPosition;
    private Vector3 interactAreaLeftPosition;
    private Vector3 interactAreaRightPosition;
    private Vector3 interactAreaTopPosition;
    private Vector3 interactAreaDownPosition;

    // Public Attributes
    public GameObject interactArea;
    public GameObject attackArea;

    private void Start()
    {
        areaRotationOnHorizontal = new Vector3(0f, 0f, ROTATION);

        playerMovement = PlayerInputs.instance.PlayerPressedMovementButtons();
        interactAreaPosition = interactArea.transform.localPosition;
    }

    private void Update()
    {
        CheckInteractButtonInput();
    }

    private void UpdateAttackArea()
    {
        attackAreaLeftPosition = attackAreaRightPosition = attackAreaTopPosition = attackAreaDownPosition = transform.position;

        attackAreaLeftPosition.x += LEFT;
        attackAreaLeftPosition.y += DEFAULT_Y;

        attackAreaRightPosition.x += RIGHT;
        attackAreaRightPosition.y += DEFAULT_Y;

        attackAreaTopPosition.y += TOP;

        attackAreaDownPosition.y += DOWN;
    }

    private void UpdateInteractArea()
    {
        interactAreaLeftPosition = interactAreaRightPosition = interactAreaTopPosition = interactAreaDownPosition = transform.position;

        interactAreaLeftPosition.x += LEFT;
        interactAreaLeftPosition.y += DEFAULT_Y;

        interactAreaRightPosition.x += RIGHT;
        interactAreaRightPosition.y += DEFAULT_Y;

        interactAreaTopPosition.y += TOP;

        interactAreaDownPosition.y += DOWN;
    }

    private void CheckAndSpawnAttackArea()
    {
        UpdateAttackArea();
        mouseWorldPos = PlayerInputs.instance.GetMousePositionInWorld();

        if (Mathf.Abs(mouseWorldPos.x - transform.position.x) > Mathf.Abs(mouseWorldPos.y - transform.position.y))
        {
            // Horizontal Axis
            if (mouseWorldPos.x > transform.position.x)
            {
                // RIGHT
                StartCoroutine(SpawnAttackArea(attackAreaRightPosition, areaRotationOnHorizontal));
            }
            else
            {
                // LEFT
                StartCoroutine(SpawnAttackArea(attackAreaLeftPosition, areaRotationOnHorizontal));
            }
        }
        else
        {
            // Vertical Axis
            if (mouseWorldPos.y > transform.position.y)
            {
                // TOP
                StartCoroutine(SpawnAttackArea(attackAreaTopPosition, Vector3.zero));
            }
            else
            {
                // DOWN
                StartCoroutine(SpawnAttackArea(attackAreaDownPosition, Vector3.zero));
            }
        }
    }

    private void CheckInteractButtonInput()
    {
        Vector2 vector2;

        if (PlayerInputs.instance.PlayerClickedMineButton())
        {
            UpdateInteractArea();
            mouseWorldPos = PlayerInputs.instance.GetMousePositionInWorld();

            vector2 = mouseWorldPos - transform.position;
            vector2.Normalize();
            vector2.y += DEFAULT_Y;
            vector2 *= 1f;

            interactArea.transform.position = vector2 + (Vector2)transform.position;
        }
    }

    public void DoSpawnAttackArea()
    {
        CheckAndSpawnAttackArea();
    }

    IEnumerator SpawnAttackArea(Vector3 areaPos, Vector3 areaRotation)
    {
        yield return new WaitForSeconds(0.1f);
        attackArea.SetActive(true);
        attackArea.transform.position = areaPos;
        currentAttackAreaRotation.eulerAngles = areaRotation;
        attackArea.transform.rotation = currentAttackAreaRotation;

        yield return new WaitForSeconds(0.1f);
        attackArea.SetActive(false);
    }
}
