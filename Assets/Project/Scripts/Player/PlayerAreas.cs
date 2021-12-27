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
    private Vector3 attackAreaLeftPosition;
    private Vector3 attackAreaRightPosition;
    private Vector3 attackAreaTopPosition;
    private Vector3 attackAreaDownPosition;
    private Vector3 interactAreaPosition;
    private Vector3 areaRotationOnHorizontal;
    private Quaternion currentAttackAreaRotation;

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
        if (PlayerInputs.instance.PlayerClickedAttackButton())
        {
            UpdateAttackArea();
            mouseWorldPos = PlayerInputs.instance.GetMousePositionInWorld();

            if (Mathf.Abs(mouseWorldPos.x - transform.position.x) > Mathf.Abs(mouseWorldPos.y - transform.position.y))
            {
                Debug.Log("Horizontal Axis");
                if (mouseWorldPos.x > transform.position.x)
                {
                    Debug.Log("RIGHT");
                    StartCoroutine(SpawnAttackArea(attackAreaRightPosition, areaRotationOnHorizontal));
                }
                else
                {
                    Debug.Log("LEFT");
                    StartCoroutine(SpawnAttackArea(attackAreaLeftPosition, areaRotationOnHorizontal));
                }
            }
            else
            {
                Debug.Log("Vertical Axis");
                if (mouseWorldPos.y > transform.position.y)
                {
                    Debug.Log("TOP");
                    StartCoroutine(SpawnAttackArea(attackAreaTopPosition, Vector3.zero));
                }
                else
                {
                    Debug.Log("DOWN");
                    StartCoroutine(SpawnAttackArea(attackAreaDownPosition, Vector3.zero));
                }
            }
        }

        playerMovement = PlayerInputs.instance.PlayerPressedMovementButtons();

        if (playerMovement.y != 0)
        {
            interactAreaPosition.x = 0f;

            if (playerMovement.y > 0)
            {
                interactAreaPosition.y = TOP;
            }
            else if (playerMovement.y < 0)
            {
                interactAreaPosition.y = DOWN;
            }

            UpdateAreas();
        }
        else if (playerMovement.x != 0 && playerMovement.y == 0)
        {
            interactAreaPosition.y = DEFAULT_Y;

            if (playerMovement.x > 0)
            {
                interactAreaPosition.x = RIGHT;
            }
            else if (playerMovement.x < 0)
            {
                interactAreaPosition.x = LEFT;
            }

            UpdateAreas();
        }
    }

    private void UpdateAreas()
    {
        interactArea.transform.localPosition = interactAreaPosition;
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

    IEnumerator SpawnAttackArea(Vector3 areaPos, Vector3 areaRotation)
    {
        //attackArea.SetActive(true);
        attackArea.transform.position = areaPos;
        currentAttackAreaRotation.eulerAngles = areaRotation;
        attackArea.transform.rotation = currentAttackAreaRotation;

        yield return null;
        //attackArea.SetActive(false);
    }
}
