using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    // Public Attributes
    public static PlayerInputs instance;

    public Vector2 mousePosition = new Vector2();
    public Vector2 mouseWorldPosition = new Vector2();
    public bool facingLeft = true;
    public bool canFlip = true;
    public bool canMove = true;
    public float playerReach = 3f;

    public GameObject selectSpotGameObject;




    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }



    // Methods
    public bool PlayerClickedMineButton()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }

    public bool PlayerClickedAttackButton()
    {
        return Input.GetKeyDown(KeyCode.Mouse1);
    }

    public void SetNewMousePosition()
    {
        mousePosition = Input.mousePosition;
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    public Vector2 GetMousePositionInWorld()
    {
        SetNewMousePosition();
        return mouseWorldPosition;
    }

    public bool PlayerPressedInteractButton()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    public bool PlayerPressedUseButton()
    {
        return Input.GetKeyDown(KeyCode.Q);
    }

    public bool PlayerPressedInventoryButton()
    {
        return Input.GetKeyDown(KeyCode.Tab);
    }

    public bool PlayerPressedQuickAccessButton()
    {
        return Input.GetKeyDown(KeyCode.LeftShift);
    }

    public bool PlayerReleasedQuickAccessButton()
    {
        return Input.GetKeyUp(KeyCode.LeftShift);
    }

    public Vector2 PlayerPressedMovementButtons()
    {
        if (canMove)
        {
            return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        else
        {
            return Vector2.zero;
        }
    }

    public Vector2 PlayerMouseScroll()
    {
        return Input.mouseScrollDelta;
    }

    public void SpawnSelectSpotAtTransform(Transform transform)
    {
        Instantiate(selectSpotGameObject, transform);
    }
}
